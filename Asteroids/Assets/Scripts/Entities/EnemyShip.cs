using System;
using Combat.Core;
using Data.EnemyShip;
using Entities.Core;
using StateMachine.Enemy.OrdinaryShip;
using UI.Enemy;
using UnityEngine;
using Zenject;

namespace Entities
{
    public class EnemyShip : ShipEntity
    {
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public EnemyTriggerColliderSettings EnemyTriggerColliderSettings { get; private set; }
        [field: SerializeField] public LayerMasks LayerMasks { get; private set; }
        [field: SerializeField] public HealthBar HealthBar { get; private set; }

        public EnemyShipData EnemyShipData { get; private set; }
        public Transform Target { get; set; }
        
        private EnemyShipStateMachine _enemyShipStateMachine;
        public override event Action<IAttackApplier> OnDied;
        
        [Inject]
        public void Construct(EnemyShipData enemyShipData)
        {
            EnemyShipData = enemyShipData;
        }

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