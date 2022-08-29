using System;
using UnityEngine;

namespace Data.EnemyShip.StatesSettings
{
    [Serializable]
    public class StateSettings
    {
        [field: SerializeField] public MovingStateSettings MovingStateSettings { get; private set; }
        [field: SerializeField] public AttackStateSettings AttackStateSettings { get; private set; }
    }
}