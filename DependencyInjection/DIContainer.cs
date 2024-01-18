using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DependencyInjection
{
    public class DIContainer
    {
        private readonly Dictionary<Type, Func<object>> _serviceFactories;
        private readonly Dictionary<Type, object> _serviceInstances;

        public DIContainer()
        {
            _serviceFactories = new Dictionary<Type, Func<object>>();
            _serviceInstances = new Dictionary<Type, object>();
        }

        public void RegisterSingleton<TService>() where TService : class
        {
            Type type = typeof(TService);
            _serviceFactories[type] = () => CreateInstance(type);
        }    

        public object Resolve(Type serviceType)
        {
            if (!_serviceInstances.TryGetValue(serviceType, out object? instance))
            {
                if (_serviceFactories.TryGetValue(serviceType, out Func<object>? factory))
                {
                    instance = factory();
                    _serviceInstances[serviceType] = instance;
                }
                else
                {
                    throw new InvalidOperationException($"Service not registered: {serviceType.Name}");
                }
            }

            return instance;
        }

        private object CreateInstance(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();

            if (constructors.Length == 0)
            {
                throw new InvalidOperationException("Type {type.Name} does not have any public constructors.");
            }

            ConstructorInfo constructor = constructors.OrderByDescending(c => c.GetParameters().Length).First();
            ParameterInfo[] parameters = constructor.GetParameters();

            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(type);
            }

            object[] paramaterInstances = parameters.Select(p => Resolve(p.ParameterType)).ToArray();
            return constructor.Invoke(paramaterInstances);
        }
    }
}
