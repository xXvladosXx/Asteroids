using System;
using AsteroidZenject;
using Core;
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

            _scoreCounter.OnScoreChanged += _scoreCounterUI.ChangeScore;
            _signalBus.Subscribe<AsteroidKilledSignal>(ChangeScore);
        }

        private void ChangeScore(AsteroidKilledSignal asteroidKilledSignal)
        {
            _scoreCounter.AddScore(asteroidKilledSignal.AsteroidEntity.Size);
        }

        public void Dispose()
        {
            _scoreCounter.OnScoreChanged -= _scoreCounterUI.ChangeScore;
            _signalBus.Unsubscribe<AsteroidKilledSignal>(ChangeScore);
        }
    }
}