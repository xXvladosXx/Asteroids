using Combat.Core;
using Data.EnemyShip;
using Data.EnemyShip.StatesSettings;
using Entities;
using StatsSystem.Core;
using UnityEngine;

namespace StateMachine.Enemy.States
{
    public class AIAttackState : AIBaseState
    {
        private readonly AttackStateSettings _attackStateSettings;
        
        public AIAttackState(EnemyShip enemyShip, BaseEnemyStateMachine baseEnemyStateMachine) : base(enemyShip, baseEnemyStateMachine)
        {
            _attackStateSettings = EnemyShip.EnemyShipData.StateSettings.AttackStateSettings;
        }

        public override void Enter()
        {
            base.Enter();
            
            BaseEnemyStateMachine.EnemyStateReusableData.RotationSpeed = _attackStateSettings.RotationSpeed;
        }

        public override void Update()
        {
            base.Update();
            
            EnemyShip.ApplyAttack(new HitData
            {
                Damage = EnemyShip.StatsData.GetStat(Stats.Damage),
                Hurtbox = EnemyShip,
                AttackApplier = EnemyShip 
            }, null);
        }

        public override void FindTarget(Collider2D[] colliders)
        {
            base.FindTarget(colliders);
            
            if (IsThereAsteroidNear()) return;
            
            RotateTowardsTargetPosition(EnemyShip.Target);
        }
    }
}