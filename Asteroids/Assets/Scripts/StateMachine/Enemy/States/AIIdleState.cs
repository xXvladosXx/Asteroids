using Entities;
using UnityEngine;

namespace StateMachine.Enemy.States
{
    public class AIIdleState: AIBaseState
    {
        public AIIdleState(EnemyShip enemyShip, BaseEnemyStateMachine baseEnemyStateMachine) : base(enemyShip, baseEnemyStateMachine) {}

        public override void Enter()
        {
            base.Enter();
            
            BaseEnemyStateMachine.ChangeState(BaseEnemyStateMachine.AIAttackState);
        }

        public override void FindTarget(Collider2D[] colliders)
        {
            
        }
    }
}