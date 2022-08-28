using System;
using System.Collections.Generic;
using AsteroidZenject;
using EnemyShipZenject;

namespace EnemiesZenject
{
    public class AsteroidRegistry
    {
        readonly List<AsteroidFacade> _asteroidFacades = new List<AsteroidFacade>();
        
        public event Action<AsteroidFacade> OnEntityAdded; 
        public event Action<AsteroidFacade> OnEntityRemoved; 

        public void AddEnemy(AsteroidFacade entityFacade)
        {
            entityFacade.OnEntitySpawned -= AddEnemy;
            
            _asteroidFacades.Add(entityFacade);
            
            OnEntityAdded?.Invoke(entityFacade);
        }

        public void RemoveEnemy(AsteroidFacade entityFacade)
        {
            entityFacade.OnEntityDestroyed-= RemoveEnemy;
            
            _asteroidFacades.Remove(entityFacade);
            
            OnEntityRemoved?.Invoke(entityFacade);
        }
    }
}