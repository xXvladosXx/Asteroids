using System;
using Entities;
using UI.Stats;
using Zenject;

namespace Controllers
{
    public class EnemyHealthController : IInitializable, IDisposable
    {
        private EnemyHealthUI _enemyHealthUI;
        private EnemyShip _enemyShip;
        
        public EnemyHealthController(EnemyHealthUI enemyHealthUI, EnemyShip enemyShip)
        {
            _enemyShip = enemyShip;
            _enemyHealthUI = enemyHealthUI;
        }
        
        public void Initialize()
        {
            _enemyHealthUI.Init(_enemyShip.Heath.MaxValue, _enemyShip.Heath.CurrentValue);
            
            _enemyShip.Heath.OnHealthChanged += _enemyHealthUI.UpdateHealth;
        }

        public void Dispose()
        {
            _enemyShip.Heath.OnHealthChanged -= _enemyHealthUI.UpdateHealth;
        }
    }
}