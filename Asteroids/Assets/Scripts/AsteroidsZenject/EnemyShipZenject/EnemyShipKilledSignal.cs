using Combat.Core;
using EnemyShipZenject;
using Entities;

namespace EnemiesZenject.EnemyShipZenject
{
    public struct EnemyShipKilledSignal
    {
        public readonly EnemyShip EnemyShip;
        public readonly IAttackApplier Destroyer;
        
        public EnemyShipKilledSignal(EnemyShip enemyShip, IAttackApplier destroyer)
        {
            EnemyShip = enemyShip;
            Destroyer = destroyer;
        }
    }
}