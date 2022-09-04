using System;
using Entities;
using StatsSystem;
using UI.Stats;
using Zenject;

namespace Controllers
{
    public class HealthController : IInitializable, IDisposable
    {
        private HealthUI _healthUI;
        private PlayerEntity _playerEntity;
        
        public HealthController(HealthUI healthUI, PlayerEntity playerEntity)
        {
            _playerEntity = playerEntity;
            _healthUI = healthUI;
        }
        
        public void Initialize()
        {
            _healthUI.Init(_playerEntity.Heath.MaxValue, _playerEntity.Heath.CurrentValue);
            
            _playerEntity.Heath.OnHealthChanged += _healthUI.UpdateHeartsHUD;
        }

        public void Dispose()
        {
            _playerEntity.Heath.OnHealthChanged -= _healthUI.UpdateHeartsHUD;
        }
    }
}