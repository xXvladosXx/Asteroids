using Data.EnemyShip;
using EnemyShipZenject;
using Entities.Core;
using StateMachine.Enemy.BaseStates;

namespace StateMachine.Enemy
{
    public class BaseEnemyStateMachine : Core.StateMachine
    {
        public readonly EnemyShip Enemy;
        public readonly EnemyStateReusableData EnemyStateReusableData;

        public AIIdleState AIIdleState { get; private set; }
        public AIAttackState AIAttackState { get; private set; }
        
        public BaseEnemyStateMachine(EnemyShip enemyShip)
        {
            Enemy = enemyShip;
            EnemyStateReusableData = new EnemyStateReusableData();
            
            AIIdleState = new AIIdleState(Enemy, this);
            AIAttackState = new AIAttackState(Enemy, this);
        }
    }
}