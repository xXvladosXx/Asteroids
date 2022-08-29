using Entities;

namespace StateMachine.Enemy.OrdinaryShip
{
    public class EnemyShipStateMachine : BaseEnemyStateMachine
    {
        private readonly EnemyShip _enemyShip;
        
        public EnemyShipStateMachine(EnemyShip enemyShip) : base(enemyShip)
        {
            _enemyShip = enemyShip;
        }
    }
}