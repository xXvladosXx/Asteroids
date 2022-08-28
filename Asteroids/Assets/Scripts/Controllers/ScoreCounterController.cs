using System;
using AsteroidZenject;
using Combat.Core;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Core;
using EnemiesZenject;
using EnemiesZenject.EnemyShipZenject;
using EnemyShipZenject;
using Entities;
using Entities.Core;
using UI.Score;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class ScoreCounterController : IInitializable, IDisposable
    {
        private ScoreCounterUI _scoreCounterUI;
        private ScoreCounter _scoreCounter;
        private SignalBus _signalBus;

        public ScoreCounterController(ScoreCounterUI scoreCounterUI, 
            ScoreCounter scoreCounter,
            SignalBus signalBus)
        {
            _scoreCounterUI = scoreCounterUI;
            _scoreCounter = scoreCounter;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _scoreCounterUI.ChangeScore(_scoreCounter.Score);

            _scoreCounter.OnScoreChanged += FindEntityToAddScore;
            _signalBus.Subscribe<AsteroidKilledSignal>(ChangeScore);
            _signalBus.Subscribe<EnemyShipKilledSignal>(ChangeScore);
        }

        private void FindEntityToAddScore(int value, IScoreCollector scoreCollector)
        {
            switch (scoreCollector)
            {
                case PlayerEntity playerEntity:
                    _scoreCounterUI.ChangeScore(scoreCollector.Points);
                    break;
                default:
                    break;;
            }
        }

        private void ChangeScore(AsteroidKilledSignal asteroidKilledSignal)
        {
            _scoreCounter.AddScore(asteroidKilledSignal.AsteroidEntity, asteroidKilledSignal.Destroyer.ScoreCollector);
        }
        
        private void ChangeScore(EnemyShipKilledSignal enemyShipKilledSignal)
        {
            _scoreCounter.AddScore(enemyShipKilledSignal.EnemyShip, enemyShipKilledSignal.Destroyer.ScoreCollector);
        }

        public void Dispose()
        {
            _scoreCounter.OnScoreChanged -= FindEntityToAddScore;
            _signalBus.Unsubscribe<AsteroidKilledSignal>(ChangeScore);
        }
    }
}