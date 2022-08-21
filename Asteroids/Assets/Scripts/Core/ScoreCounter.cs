using System;
using Data.Score;
using UnityEngine;

namespace Core
{
    [Serializable]
    public sealed class ScoreCounter
    {
        [field: SerializeField] public ScoreData ScoreData { get; private set; }
        public int Score { get; private set; }
        
        public event Action<int> OnScoreChanged;
        
        public ScoreCounter(ScoreData scoreData)
        {
            Score = 0;
            ScoreData = scoreData;
        }

        public void AddScore(float asteroidSize)
        {
            if (ScoreData.MaxSize < asteroidSize)
            {
                Score += ScoreData.MaxReward;
            }
            else if (ScoreData.MaxSize > asteroidSize && asteroidSize > ScoreData.OrdinarySize)
            {
                Score += ScoreData.OrdinaryReward;
            }
            else if(asteroidSize < ScoreData.OrdinarySize)
            {
                Score += ScoreData.MinReward;
            }
            
            OnScoreChanged?.Invoke(Score);
        }
    }
}