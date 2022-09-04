using Data.Asteroid;
using Data.EnemyShip;
using Data.Player;
using UnityEngine;

namespace Data.Difficulties
{
    [CreateAssetMenu(menuName = "Data/DifficultyData")]
    public class DifficultyData : ScriptableObject
    {
        [field: SerializeField] public AsteroidData AsteroidData { get; private set; }
        [field: SerializeField] public PlayerSettingsSO PlayerSettingsSo { get; private set; }
        [field: SerializeField] public EnemyShipData EnemyShipData { get; private set; }
        [field: SerializeField] public AsteroidSpawnerData AsteroidSpawnerData { get; private set; }
        [field: SerializeField] public EnemyShipSpawnerData EnemyShipSpawnerData { get; private set; }
    }
}