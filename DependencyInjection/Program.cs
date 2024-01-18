
using System.ComponentModel;

namespace DependencyInjection
{
    public class Program
    {
        public static DIContainer DIContainer = new DIContainer();

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }

        public void Run()
        {
            RegisterDI();

            EntityManager entityManager = (EntityManager)DIContainer.Resolve(typeof(EntityManager));
        }

        private void RegisterDI()
        {
            DIContainer = new DIContainer();

            DIContainer.RegisterSingleton<Minimap>();
            DIContainer.RegisterSingleton<UnitManager>();
            DIContainer.RegisterSingleton<MouseInteraction>();
            DIContainer.RegisterSingleton<EntityManager>();
        }
    }
}
