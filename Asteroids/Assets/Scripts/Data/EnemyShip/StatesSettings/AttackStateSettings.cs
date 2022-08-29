using UnityEngine;

namespace Data.EnemyShip.StatesSettings
{
    [CreateAssetMenu(menuName = ("State/AttackStateSettings"))]
    public class AttackStateSettings : ScriptableObject
    {
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}