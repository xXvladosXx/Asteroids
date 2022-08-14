using Entities.Core;
using UnityEngine;
using Utilities.Extensions;

namespace Spawners.Core
{
    public abstract class EntitySpawner : MonoBehaviour
    {
        [field: SerializeField] public AliveEntity EntityToSpawn { get; private set; }
        [field: SerializeField] public int SpawnAmount { get; private set; }
        [field: SerializeField] public float SpawnRate { get; private set; }

        private void Start()
        {
            this.CallWithRepeat(Spawn, SpawnRate);
        }

        public abstract void Spawn();
    }
}