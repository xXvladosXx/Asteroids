using System;
using Combat.Core;
using Entities;
using Zenject;

namespace AsteroidsZenject.PlayerZenject
{
    public class PlayerDeathHandler : IInitializable, IDisposable
    {
        private readonly PlayerEntity _playerEntity;

        public event Action OnPlayerDied;
        
        public PlayerDeathHandler(PlayerEntity playerEntity)
        {
            _playerEntity = playerEntity;
        }
        
        public void Initialize()
        {
            _playerEntity.Heath.OnDied += HandleDeath;
        }

        private void HandleDeath(IAttackApplier obj)
        {
            OnPlayerDied?.Invoke();
        }

        public void Dispose()
        {
            _playerEntity.Heath.OnDied += HandleDeath;
        }
    }
}