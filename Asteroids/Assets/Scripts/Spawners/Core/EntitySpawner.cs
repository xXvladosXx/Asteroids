using Entities.Core;
using UnityEngine;
using Utilities.Extensions;

namespace Spawners.Core
{
    public abstract class EntitySpawner : Spawner
    {
        [field: SerializeField] public Entity EntityToSpawn { get; private set; }
    }
}