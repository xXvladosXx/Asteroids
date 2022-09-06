using System;
using AsteroidsZenject;
using AsteroidsZenject.PlayerZenject;
using Combat.Core;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Entities;
using Entities.Core;
using Saving;
using Score;
using UI.Score;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class PlayerScoreCounterController : IInitializable, IDisposable
    {
        private readonly SaveSystem _saveSystem;
        private readonly PlayerDeathHandler _playerDeathHandler;

        private ScoreCounterUI _scoreCounterUI;
        private ScoreCounter _scoreCounter;
        private SignalBus _signalBus;

        public PlayerScoreCounterController(ScoreCounterUI scoreCounterUI,
            ScoreCounter scoreCounter,
            SignalBus signalBus,
            SaveSystem saveSystem,
            PlayerDeathHandler playerDeathHandler)
        {
            _scoreCounterUI = scoreCounterUI;
            _scoreCounter = scoreCounter;
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _playerDeathHandler = playerDeathHandler;
        }
        
        public void Initialize()
        {
            _scoreCounterUI.ChangeScore(_scoreCounter.Score);

            _scoreCounter.OnScoreChanged += FindEntityToAddScore;
            _playerDeathHandler.OnPlayerDied += SaveScore;
            
            _signalBus.Subscribe<EntityKilledSignal>(ChangeScore);
        }

        private void SaveScore()
        {
            _saveSystem.TryToRegisterBestResult(new SaveData
            {
                Score = _scoreCounter.Score
            });
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
            _playerDeathHandler.OnPlayerDied -= SaveScore;

            _signalBus.Unsubscribe<EntityKilledSignal>(ChangeScore);
        }
    }
}