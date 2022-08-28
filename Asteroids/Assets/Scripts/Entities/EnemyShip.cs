using System;
using Combat.Core;
using Core;
using Data.EnemyShip;
using Entities;
using Entities.Core;
using Pathfinding;
using StateMachine.Enemy.OrdinaryShip;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace EnemyShipZenject
{
    public class EnemyShip : ShipEntity
    {
        [field: SerializeField] public StateSettings StateSettings { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public EnemyTriggerColliderSettings EnemyTriggerColliderSettings { get; private set; }
        [field: SerializeField] public LayerMasks LayerMasks { get; private set; }
        [field: SerializeField] public HealthBar HealthBar { get; private set; }

        public Transform Target { get; set; }
        
        private EnemyShipStateMachine _enemyShipStateMachine;
        public override event Action<IAttackApplier> OnDied;

        public override void Construct()
        {
            base.Construct();
            
            HealthBar.Init(Heath);
            
            Heath.OnDied += Die;
        }

        public override void Die(IAttackApplier attackApplier)
        {
            Heath.OnDied -= Die;

            OnDied?.Invoke(attackApplier);
        }

        protected override void OnAwake()
        {
            _enemyShipStateMachine = new EnemyShipStateMachine(this);
            EnemyTriggerColliderSettings.Initialize();
        }

        private void Start()
        {
            _enemyShipStateMachine.ChangeState(_enemyShipStateMachine.AIIdleState);
        }

        private void Update()
        {
            _enemyShipStateMachine.Update();
        }

        private void FixedUpdate()
        {
            _enemyShipStateMachine.PhysicsUpdate();
        }
    }
}