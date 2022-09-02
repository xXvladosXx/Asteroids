using UnityEngine;

namespace Data.Asteroid
{
    [CreateAssetMenu(menuName = "Data/AsteroidSpawnerData")]
    public class AsteroidSpawnerData : ScriptableObject
    {
        [field: SerializeField] public float SpawnDistance { get; private set; } = 10;
        [field: SerializeField] public float DirectionVariance { get; private set; } = 10;
        [field: SerializeField] public float NumEnemiesIncreaseRate { get; private set; } = 1;
        [field: SerializeField] public float NumEnemiesStartAmount { get; private set; } = 10;
        [field: SerializeField] public float MinDelayBetweenSpawns { get; private set; } = 0.5f;
        [field: SerializeField] public float AsteroidLifetime { get; private set; } = 12f;
    }
}