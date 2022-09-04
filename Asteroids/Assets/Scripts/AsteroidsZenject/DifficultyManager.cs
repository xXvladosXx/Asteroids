using System;
using Data.Difficulties;
using UnityEngine;

namespace AsteroidsZenject
{
    public class DifficultyManager 
    {
        public DifficultiesData DifficultiesData { get; private set; }
        public DifficultyData DifficultyData { get; private set; }

        public DifficultyManager(DifficultiesData difficultiesData)
        {
            DifficultiesData = difficultiesData;
            DifficultyData = difficultiesData.DifficultyDatas[0];
        }
        
        public void ChangeDifficulty(int difficultyLevel)
        {
            DifficultyData = DifficultiesData.DifficultyDatas[difficultyLevel];
        }
    }
}