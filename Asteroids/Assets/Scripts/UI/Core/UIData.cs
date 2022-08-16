using System;
using Core;
using Entities;

namespace UI.Core
{
    [Serializable]
    public class UIData
    {
        public PlayerEntity Player { get; set; }
        public ScoreCounter ScoreCounter { get; set; }
        public GameContext GameContext { get; set; }
    }
}