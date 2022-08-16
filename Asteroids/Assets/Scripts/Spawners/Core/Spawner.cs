using UnityEngine;
using Utilities.Extensions;

namespace Spawners.Core
{
    public abstract class Spawner : MonoBehaviour 
    {
        [field: SerializeField] public int SpawnAmount { get; private set; }
        [field: SerializeField] public float SpawnRate { get; private set; }

        protected virtual void Awake()
        {
        }
        private void Start()
        {
            this.CallWithRepeat(Spawn, SpawnRate);
        }

        public abstract void Spawn();
    }
}