using System;
using UnityEngine;

namespace Data.Camera
{
    [Serializable]
    public class CameraShakerData
    {
        [field: SerializeField] public float PlayerDamageMagnitude { get; private set; }
        [field: SerializeField] public float PlayerDamageTime { get; private set; }
        
        [field: SerializeField] public float PlayerDeathMagnitude { get; private set; }
        [field: SerializeField] public float PlayerDeathTime { get; private set; }
    }
}