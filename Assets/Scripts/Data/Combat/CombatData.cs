using System;
using UnityEngine;

namespace Data.Combat
{
    [Serializable]
    public class CombatData
    {
        [field: SerializeField] public global::Combat.Bullet BulletPrefab { get; private set; }
    }
}