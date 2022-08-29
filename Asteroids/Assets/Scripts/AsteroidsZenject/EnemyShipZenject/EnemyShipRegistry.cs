using System;
using System.Collections.Generic;

namespace AsteroidsZenject.EnemyShipZenject
{
    public class EnemyShipRegistry
    {
        readonly List<EnemyShipFacade> _enemyShipFacades = new List<EnemyShipFacade>();
        
        public event Action<EnemyShipFacade> OnEntityAdded; 
        public event Action<EnemyShipFacade> OnEntityRemoved; 

        public void AddEnemy(EnemyShipFacade entityFacade)
        {
            _enemyShipFacades.Add(entityFacade);
            
            OnEntityAdded?.Invoke(entityFacade);
        }

        public void RemoveEnemy(EnemyShipFacade entityFacade)
        {
            _enemyShipFacades.Remove(entityFacade);
            
            OnEntityRemoved?.Invoke(entityFacade);
        }
    }
}