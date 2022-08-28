using System;
using Combat.Core;
using Core;
using TMPro;
using UI.Core;
using UnityEngine;

namespace UI.Score
{
    public sealed class ScoreCounterUI : StaticUIElement
    {
        [SerializeField] private TextMeshProUGUI _score;

        public void ChangeScore(int value)
        {
            _score.text = value.ToString();
        }

    }
}