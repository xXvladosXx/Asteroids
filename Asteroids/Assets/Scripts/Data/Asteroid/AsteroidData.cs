using System;
using UnityEngine;

namespace Data.Asteroid
{
    [Serializable]
    public sealed class AsteroidData 
    {
        [field: SerializeField] public Sprite[] PossibleSprites { get; private set; }
        [field: SerializeField] public float SizeToSplit { get; private set; } 
        [field: SerializeField] public float MaxSize { get; private set; } 
        [field: SerializeField] public float MinSize { get; private set; } 
        [field: SerializeField] public float MinSpeed { get; private set; } 
        [field: SerializeField] public float MaxSpeed { get; private set; } 
        [field: SerializeField] public float Lifetime { get; private set; } 
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
    }
}