using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace DependencyInjection
{
    public class EntityManager
    {
        MouseInteraction _mouseInteraction;
        UnitManager _unitManager;
        Minimap _minimap;

        public EntityManager(MouseInteraction mouseInteraction, UnitManager unitManager, Minimap minimap)
        {
            _mouseInteraction = mouseInteraction;
            _unitManager = unitManager;
            _minimap = minimap;
        }
    }
}
