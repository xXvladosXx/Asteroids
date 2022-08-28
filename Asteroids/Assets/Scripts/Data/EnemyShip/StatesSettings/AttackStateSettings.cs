using UnityEngine;

namespace Data.EnemyShip
{
    [CreateAssetMenu(menuName = ("State/AttackStateSettings"))]
    public class AttackStateSettings : ScriptableObject
    {
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}