using System;
using Combat.Core;
using Data.Score;
using EnemyShipZenject;
using Entities;
using Entities.Core;
using UnityEngine;

namespace Core
{
    [Serializable]
    public sealed class ScoreCounter
    {
        [field: SerializeField] public ScoreData ScoreData { get; private set; }
        public int Score { get; private set; }
        
        public event Action<int, IScoreCollector> OnScoreChanged;
        
        public ScoreCounter(ScoreData scoreData)
        {
            Score = 0;
            ScoreData = scoreData;
        }

        public void AddScore(Entity entity, IScoreCollector scoreCollector)
        {
            if(scoreCollector == null) return;

            switch (entity)
            {
                case AsteroidEntity asteroidEntity:
                    CalculateScorePerAsteroid(asteroidEntity, scoreCollector);
                    break;
                case EnemyShip enemyShip:
                    CalculateScorePerEnemyShip(enemyShip, scoreCollector);
                    break;
            }
            
            OnScoreChanged?.Invoke(scoreCollector.Points, scoreCollector);
        }

        private void CalculateScorePerEnemyShip(EnemyShip enemyShip, IScoreCollector scoreCollector)
        {
            scoreCollector.Points += ScoreData.EnemyShipReward;
        }

        private void CalculateScorePerAsteroid(AsteroidEntity asteroidEntity, IScoreCollector scoreCollector)
        {
            if (ScoreData.MaxSize < asteroidEntity.Size)
            {
                scoreCollector.Points += ScoreData.MaxReward;
            }
            else if (ScoreData.MaxSize > asteroidEntity.Size && asteroidEntity.Size > ScoreData.OrdinarySize)
            {
                scoreCollector.Points += ScoreData.OrdinaryReward;
            }
            else if(asteroidEntity.Size < ScoreData.OrdinarySize)
            {
                scoreCollector.Points += ScoreData.MinReward;
            }
        }
    }
}