using System;
using Data.Score;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class ScoreCounter
    {
        [field: SerializeField] public ScoreData ScoreData { get; private set; }
        public int Score { get; private set; }
        
        public event Action OnScoreChanged;
        
        public ScoreCounter(ScoreData scoreData)
        {
            Score = 0;
            ScoreData = scoreData;
        }

        public void AddScore(float size)
        {
            if (ScoreData.MaxSize < size)
            {
                Score += ScoreData.MaxReward;
            }
            else if (ScoreData.MaxSize > size && size > ScoreData.OrdinarySize)
            {
                Score += ScoreData.OrdinaryReward;
            }
            else if(size < ScoreData.OrdinarySize)
            {
                Score += ScoreData.MinReward;
            }
            
            OnScoreChanged?.Invoke();
        }
    }
}