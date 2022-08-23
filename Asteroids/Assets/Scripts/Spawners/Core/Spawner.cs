using UnityEngine;
using Utilities.Extensions;

namespace Spawners.Core
{
    public abstract class Spawner  
    {
        [field: SerializeField] public int SpawnAmount { get; private set; }
        [field: SerializeField] public float SpawnRate { get; private set; }

        public abstract void Spawn();
    }
}