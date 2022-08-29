using AsteroidsZenject.ExplosionZenject;
using AudioSystem;
using Combat.Core;
using Entities;
using Zenject;

namespace AsteroidsZenject.AsteroidZenject
{
    public class AsteroidDeathHandler
    {
        private readonly AsteroidFacade _asteroidFacade;
        private readonly SignalBus _signalBus;
        private readonly AsteroidEntity _asteroidEntity;
        private readonly EnemyExplosion.Factory _explosionFactory;
        private readonly AudioManager _audioManager;

        public AsteroidDeathHandler(AsteroidFacade asteroidFacade,
            SignalBus signalBus,
            AsteroidEntity asteroidEntity,
            EnemyExplosion.Factory explosionFactory,
            AudioManager audioManager)
        {
            _asteroidFacade = asteroidFacade;
            _signalBus = signalBus;
            _asteroidEntity = asteroidEntity;
            _explosionFactory = explosionFactory;
            _audioManager = audioManager;
        }
        
        public void Die(IAttackApplier attackApplier)
        {
            var explosion = _explosionFactory.Create();
            explosion.transform.position = _asteroidFacade.AsteroidEntity.transform.position;
            
            _signalBus.Fire(new EntityKilledSignal(_asteroidEntity, attackApplier));

            _audioManager.PlayEffectSound(_asteroidEntity.AsteroidData.AudioClip);
            _asteroidFacade.Dispose();
        }
    }
}