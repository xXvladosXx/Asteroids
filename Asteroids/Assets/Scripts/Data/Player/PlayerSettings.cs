using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(menuName = "Data/PlayerData")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 4;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 2;
    }
}