using UnityEngine;

namespace Data.EnemyShip
{
    [CreateAssetMenu(menuName = "Data/EnemyShipSpawnerData")]
    public class EnemyShipSpawnerData : ScriptableObject
    {
        [field: SerializeField] public Vector3[] PointsToSpawn { get; private set; }
        [field: SerializeField] public float NumEnemiesIncreaseRate { get; private set; }
        [field: SerializeField] public float NumEnemiesStartAmount { get; private set; }
        [field: SerializeField] public float MinDelayBetweenSpawns { get; private set; } = 5f;
    }
}

