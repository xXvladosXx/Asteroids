using System;
using UnityEngine;

namespace Data.EnemyShip
{
    [Serializable]
    public class StateSettings
    {
        [field: SerializeField] public LayerMasks LayerMasks { get; private set; }
        [field: SerializeField] public MovingStateSettings MovingStateSettings { get; private set; }
        [field: SerializeField] public AttackStateSettings AttackStateSettings { get; private set; }
    }
}