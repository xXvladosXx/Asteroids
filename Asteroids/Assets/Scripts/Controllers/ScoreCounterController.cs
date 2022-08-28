using System;
using AsteroidsZenject.EnemyShipZenject;
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
            _signalBus.Subscribe<EntityKilledSignal>(ChangeScore);
        }

        private void FindEntityToAddScore(int value, IScoreCollector scoreCollector)
        {
            switch (scoreCollector)
            {
                case PlayerEntity playerEntity:
                    _scoreCounterUI.ChangeScore(playerEntity.Points);
                    break;
            }
        }

        private void ChangeScore(EntityKilledSignal entityKilledSignal)
        {
            if(entityKilledSignal.AttackApplier == null) return;

            _scoreCounter.AddScore(entityKilledSignal.Entity, entityKilledSignal.AttackApplier.ScoreCollector);
        }

        public void Dispose()
        {
            _scoreCounter.OnScoreChanged -= FindEntityToAddScore;
            _signalBus.Unsubscribe<EntityKilledSignal>(ChangeScore);
        }
    }
}