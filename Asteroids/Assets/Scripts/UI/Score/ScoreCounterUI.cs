using System;
using Core;
using TMPro;
using UI.Core;
using UnityEngine;

namespace UI.Score
{
    public sealed class ScoreCounterUI : StaticUIElement
    {
        [SerializeField] private TextMeshProUGUI _score;

        private ScoreCounter _scoreCounter;

        public override void Init(UIData uiData)
        {
            base.Init(uiData);

            _scoreCounter = uiData.ScoreCounter;
            
            _score.text = _scoreCounter.Score.ToString();

            _scoreCounter.OnScoreChanged += ChangeScore;
        }

        private void ChangeScore()
        {
            _score.text = _scoreCounter.Score.ToString();
        }

        private void OnDisable()
        {
            _scoreCounter.OnScoreChanged -= ChangeScore;
        }
    }
}