using Combat.Core;
using Entities.Core;

namespace Entities
{
    public class OrdinaryShip : ShipEntity, IEnemy
    {
        public override void Die()
        {
            
        }

        public override void ApplyAttack(HitData hitData)
        {
        }

        public PlayerEntity PlayerEntity { get; set; }
    }
}