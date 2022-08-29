using System;
using AsteroidsZenject.ExplosionZenject;
using AudioSystem;
using Combat.Core;
using Zenject;

namespace AsteroidsZenject.EnemyShipZenject
{
    public class EnemyShipDeathHandler : IInitializable, IDisposable
    {
        private readonly EnemyShipFacade _enemyShipFacade;
        private readonly SignalBus _signalBus;
        private readonly EnemyExplosion.Factory _explosionFactory;
        private readonly AudioManager _audioManager;

        public EnemyShipDeathHandler(EnemyShipFacade enemyShipFacade,
            SignalBus signalBus,
            EnemyExplosion.Factory explosionFactory,
            AudioManager audioManager)
        {
            _enemyShipFacade = enemyShipFacade;
            _signalBus = signalBus;
            _explosionFactory = explosionFactory;
            _audioManager = audioManager;
        }
        
        public void Initialize()
        {
            _enemyShipFacade.EnemyShip.OnDied += Die;
        }

        private void Die(IAttackApplier attackApplier)
        {
            var explosion = _explosionFactory.Create();
            explosion.transform.position = _enemyShipFacade.EnemyShip.transform.position;
            
            _signalBus.Fire(new EntityKilledSignal(_enemyShipFacade.EnemyShip, attackApplier));
            
            _audioManager.PlayEffectSound(_enemyShipFacade.EnemyShip.EnemyShipData.AudioClip);
            _enemyShipFacade.Dispose();
        }

        public void Dispose()
        {
            _enemyShipFacade.EnemyShip.OnDied -= Die;
        }
    }
}