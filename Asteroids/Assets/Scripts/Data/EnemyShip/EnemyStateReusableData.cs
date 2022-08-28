using UnityEngine;

namespace Data.EnemyShip
{
    public class EnemyStateReusableData
    {
        public float RotationSpeed { get; set; }
        public Vector3 AsteroidPreviousPosition { get; set; }
        public Vector3 AsteroidLastMoveDirection { get; set; }
    }
}