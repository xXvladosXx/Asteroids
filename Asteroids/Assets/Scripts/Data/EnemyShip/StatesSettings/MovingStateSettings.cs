using System;
using UnityEngine;

namespace Data.EnemyShip
{
    [CreateAssetMenu(menuName = ("State/MovingStateSettings"))]
    public class MovingStateSettings : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float StoppingDistanceToPlayer { get; private set; }
    }
}