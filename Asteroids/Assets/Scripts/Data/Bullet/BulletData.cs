using System;
using UnityEngine;

namespace Data.Bullet
{
    [Serializable]
    public sealed class BulletData
    {
        [field: SerializeField] public float BulletSpeed { get; private set; } = 250;
        [field: SerializeField] public float MaxLifeTime { get; private set; } = 8;
    }
}