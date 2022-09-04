using UnityEngine;

namespace Data.Asteroid
{
    [CreateAssetMenu(menuName = "Data/AsteroidData")]
    public class AsteroidData : ScriptableObject
    {
        [field: SerializeField] public Sprite[] PossibleSprites { get; private set; }
        [field: SerializeField] public float SizeToSplit { get; private set; } 
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public float MaxSize { get; private set; } 
        [field: SerializeField] public float MinSize { get; private set; } 
        [field: SerializeField] public float MinSpeed { get; private set; } 
        [field: SerializeField] public float MaxSpeed { get; private set; } 
        [field: SerializeField] public float Lifetime { get; private set; } 
    }
}