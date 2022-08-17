using System;
using UnityEngine;

namespace Data.Player
{
    [Serializable]
    public sealed class PlayerSettings
    {
        [field: SerializeField] public float MovementSpeed { get; private set; } = 4;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 2;
    }
}