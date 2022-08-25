using System;
using EnemiesZenject;

namespace Entities.Core
{
    public interface IEnemy
    {
        public event Action<EntityFacade> OnEntitySpawned;
        public event Action<EntityFacade> OnEntityDestroyed;
        PlayerEntity PlayerEntity { get; set; }
    }
}