using AsteroidsZenject.EnemyShipZenject;
using AsteroidsZenject.ExplosionZenject;
using Combat.Core;
using Entities;
using Zenject;

namespace AsteroidZenject
{
    public class AsteroidDeathHandler
    {
        private readonly AsteroidFacade _asteroidFacade;
        private readonly SignalBus _signalBus;
        private readonly AsteroidEntity _asteroidEntity;
        private readonly EnemyExplosion.Factory _explosionFactory;

        public AsteroidDeathHandler(AsteroidFacade asteroidFacade,
            SignalBus signalBus,
            AsteroidEntity asteroidEntity,
            EnemyExplosion.Factory explosionFactory)
        {
            _asteroidFacade = asteroidFacade;
            _signalBus = signalBus;
            _asteroidEntity = asteroidEntity;
            _explosionFactory = explosionFactory;
        }
        
        public void Die(IAttackApplier attackApplier)
        {
            var explosion = _explosionFactory.Create();
            explosion.transform.position = _asteroidFacade.AsteroidEntity.transform.position;
            
            _signalBus.Fire(new EntityKilledSignal(_asteroidEntity, attackApplier));

            _asteroidFacade.Dispose();
        }
    }
}