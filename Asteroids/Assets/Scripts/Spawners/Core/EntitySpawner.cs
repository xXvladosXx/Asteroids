using EnemiesZenject.AsteroidZenject;
using Entities.Core;
using UnityEngine;
using Utilities.Extensions;

namespace Spawners.Core
{
    public abstract class EntitySpawner : Spawner
    {
        protected EntityRegistry EntityRegistry;

        public EntitySpawner(EntityRegistry entityRegistry)
        {
            EntityRegistry = entityRegistry;
        }
    }
}