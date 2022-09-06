using System;
using System.Collections.Generic;
using AsteroidsZenject.EnemyShipZenject;
using Combat.Core;
using Entities;
using Score;
using UI.Enemy;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class EnemiesController : IInitializable, IDisposable
    {
        private readonly EnemyShipRegistry _enemyShipRegistry;
        private readonly EnemiesUI _enemiesUI;
        private readonly ScoreCounter _scoreCounter;
        
        private Dictionary<EnemyShip, EnemyUI> _enemyUis = new Dictionary<EnemyShip, EnemyUI>();

        public EnemiesController(EnemiesUI enemiesUI, EnemyShipRegistry enemyShipRegistry,
            ScoreCounter scoreCounter)
        {
            _enemyShipRegistry = enemyShipRegistry;
            _enemiesUI = enemiesUI;
            _scoreCounter = scoreCounter;
        }

        public void Initialize()
        {
            _enemiesUI.Init();

            _scoreCounter.OnScoreChanged += TryToAddScoreToEnemy;
            _enemyShipRegistry.OnEntityAdded += AddToUIEnemy;
            _enemyShipRegistry.OnEntityRemoved += RemoveFromUIEnemy;
        }
        
        private void TryToAddScoreToEnemy(int value, IScoreCollector scoreCollector)
        {
            if (scoreCollector is EnemyShip enemyShip)
            {
                _enemyUis.TryGetValue(enemyShip, out var index);
                
                _enemiesUI.RefreshData(index, new EnemyData(scoreCollector.Points));
            }
        }

        private void AddToUIEnemy(EnemyShipFacade enemyShipFacade)
        {
            var enemyUI = _enemiesUI.AddNewEnemyUI();
            
            _enemyUis.Add(enemyShipFacade.EnemyShip, enemyUI);
        }
        
        private void RemoveFromUIEnemy(EnemyShipFacade enemyShipFacade)
        {
            _enemyUis.TryGetValue(enemyShipFacade.EnemyShip, out var id);
            _enemiesUI.RemoveEnemyUI(id);
            
            _enemyUis.Remove(enemyShipFacade.EnemyShip);
        }

       

        public void Dispose()
        {
            _scoreCounter.OnScoreChanged -= TryToAddScoreToEnemy;
            _enemyShipRegistry.OnEntityAdded -= AddToUIEnemy;
            _enemyShipRegistry.OnEntityRemoved -= RemoveFromUIEnemy;
        }
    }
}