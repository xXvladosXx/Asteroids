using System;
using UnityEngine;

namespace Data.Bullet
{
    [Serializable]
    public sealed class ProjectileData
    {
        [field: SerializeField] public float ProjectileSpeed { get; private set; } = 250;
        [field: SerializeField] public float MaxLifeTime { get; private set; } = 8;
        [field: SerializeField] public float TimeBetweenShooting { get; private set; } = 8;
    }
}