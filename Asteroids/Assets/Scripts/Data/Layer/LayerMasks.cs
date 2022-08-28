using System;
using UnityEngine;

namespace Data.EnemyShip
{
    [Serializable]
    public class LayerMasks
    {
        [field: SerializeField] public LayerMask Boundary { get; private set; }
        [field: SerializeField] public LayerMask Asteroid { get; private set; }
    }
}