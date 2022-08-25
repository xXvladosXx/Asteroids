using EnemyShipZenject;
using Entities.Core;
using StateMachine.Enemy.BaseStates;

namespace StateMachine.Enemy
{
    public class BaseEnemyStateMachine : Core.StateMachine
    {
        public readonly EnemyShip Enemy;

        public AIIdleState AIIdleState { get; private set; }
        public AIAttackState AIAttackState { get; private set; }
        
        public BaseEnemyStateMachine(EnemyShip enemyShip)
        {
            Enemy = enemyShip;

            AIIdleState = new AIIdleState(Enemy, this);
            AIAttackState = new AIAttackState(Enemy, this);
        }
    }
}