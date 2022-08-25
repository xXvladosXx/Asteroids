using System;
using Combat.Core;
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

        [Inject]
        public void Construct(PlayerEntity playerEntity)
        {
            Target = playerEntity.transform;
        }

        public Transform Target { get; private set; }

        private EnemyShipStateMachine _enemyShipStateMachine;

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

        public override void Die()
        {
        }
    }
}