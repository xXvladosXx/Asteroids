using System.Collections.Generic;
using AsteroidZenject;

namespace EnemiesZenject.AsteroidZenject
{
    public class EntityRegistry
    {
        readonly List<EntityFacade> _entityFacades = new List<EntityFacade>();

        public void AddEnemy(EntityFacade entityFacade)
        {
            _entityFacades.Add(entityFacade);
        }

        public void RemoveEnemy(EntityFacade entityFacade)
        {
            _entityFacades.Remove(entityFacade);
        }

        
    }
}