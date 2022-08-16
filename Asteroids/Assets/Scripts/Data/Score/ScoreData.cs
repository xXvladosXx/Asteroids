using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Score
{
    [Serializable]
    public class ScoreData
    {
        [field: SerializeField] public float MinSize { get; private set; }
        [field: SerializeField] public float MaxSize { get; private set; }
        [field: SerializeField] public float OrdinarySize { get; private set; }
        
        [field: SerializeField] public int MinReward { get; private set; }
        [field: SerializeField] public int MaxReward { get; private set; }
        [field: SerializeField] public int OrdinaryReward { get; private set; }
    }
}