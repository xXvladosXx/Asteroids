using System;
using UnityEngine;

namespace Data.EnemyShip
{
    [Serializable]
    public class EnemyTriggerColliderSettings
    {
        [field: SerializeField] public BoxCollider2D BoundaryCheckCollider { get; private set; }
        [field: SerializeField] public BoxCollider2D AsteroidCheckCollider { get; private set; }
        
        public Vector2 BoundaryCheckColliderExtents { get; private set; }
        public Vector2 AsteroidCheckColliderExtents { get; private set; }

        public void Initialize()
        {
            BoundaryCheckColliderExtents = BoundaryCheckCollider.bounds.extents;
            AsteroidCheckColliderExtents = AsteroidCheckCollider.bounds.extents;
        }
    }
}