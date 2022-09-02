using Data.EnemyShip.StatesSettings;
using UnityEngine;

namespace Data.EnemyShip
{
    [CreateAssetMenu(menuName = "Data/EnemyShipData")]
    public class EnemyShipData : ScriptableObject
    {
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public StateSettings StateSettings { get; private set; }
    }
}