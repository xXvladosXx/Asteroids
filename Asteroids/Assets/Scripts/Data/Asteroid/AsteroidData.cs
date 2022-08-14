using System;
using UnityEngine;

namespace Data.Asteroid
{
    [Serializable]
    public class AsteroidData 
    {
        [field: SerializeField] public Sprite[] PossibleSprites { get; private set; }
        [field: SerializeField] public float PossibleSize { get; private set; } 

        [field: SerializeField] public float MaxSize { get; private set; } 
        [field: SerializeField] public float MinSize { get; private set; } 
    }
}