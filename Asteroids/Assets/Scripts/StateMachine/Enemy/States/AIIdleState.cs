using EnemyShipZenject;
using Entities.Core;
using StateMachine.Core;
using UnityEngine;

namespace StateMachine.Enemy.BaseStates
{
    public class AIIdleState: AIBaseState
    {
        public AIIdleState(EnemyShip enemyShip, BaseEnemyStateMachine baseEnemyStateMachine) : base(enemyShip, baseEnemyStateMachine) {}

        public override void Enter()
        {
            base.Enter();
            
            BaseEnemyStateMachine.ChangeState(BaseEnemyStateMachine.AIAttackState);
        }
    }
}