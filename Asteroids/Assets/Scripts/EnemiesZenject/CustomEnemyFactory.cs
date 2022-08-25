using AsteroidZenject;
using EnemyShipZenject;
using Entities.Core;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Zenject;

namespace EnemiesZenject
{
    public class CustomEnemyFactory : IFactory<IEnemy>
    {
        private EnemyShipFacade.Factory _enemyShipFactory;
        private AsteroidFacade.Factory _asteroidFactory;
        
        public CustomEnemyFactory(EnemyShipFacade.Factory enemyShipFactory,
            AsteroidFacade.Factory asteroidFactory)
        {
            _enemyShipFactory = enemyShipFactory;
            _asteroidFactory = asteroidFactory;;
        }
        public IEnemy Create()
        {
            var ship = _enemyShipFactory.Create();
            Debug.Log("Created");
            ship.Construct();

            return ship;

        }

    }
}