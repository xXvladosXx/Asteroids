using Combat.Core;
using Entities.Core;

namespace AsteroidsZenject.EnemyShipZenject
{
    public class EntityKilledSignal
    {
        public readonly Entity Entity;
        public readonly IAttackApplier AttackApplier;
        
        public EntityKilledSignal(Entity entity, IAttackApplier attackApplier)
        {
            Entity = entity;
            AttackApplier = attackApplier;
        }
    }
}