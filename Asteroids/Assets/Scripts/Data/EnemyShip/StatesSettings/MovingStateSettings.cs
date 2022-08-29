using UnityEngine;

namespace Data.EnemyShip.StatesSettings
{
    [CreateAssetMenu(menuName = ("State/MovingStateSettings"))]
    public class MovingStateSettings : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float StoppingDistanceToPlayer { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}