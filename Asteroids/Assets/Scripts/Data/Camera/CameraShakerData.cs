using System;
using UnityEngine;

namespace Data.Camera
{
    [Serializable]
    public class CameraShakerData
    {
        [field: SerializeField] public float Magnitude { get; private set; }
        [field: SerializeField] public float Time { get; private set; }
    }
}