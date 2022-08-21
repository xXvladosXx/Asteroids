using System;
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
        
        public ScoreCounterController(ScoreCounterUI scoreCounterUI, ScoreCounter scoreCounter)
        {
            _scoreCounterUI = scoreCounterUI;
            _scoreCounter = scoreCounter;
        }
        
        public void Initialize()
        {
            _scoreCounterUI.ChangeScore(_scoreCounter.Score);

            _scoreCounter.OnScoreChanged += _scoreCounterUI.ChangeScore;
        }

        public void Dispose()
        {
            _scoreCounter.OnScoreChanged -= _scoreCounterUI.ChangeScore;
        }
    }
}