using Data.EnemyShip;
using EnemyShipZenject;
using Entities.Core;
using StateMachine.Core;
using UnityEngine;

namespace StateMachine.Enemy.BaseStates
{
    public abstract class AIBaseState : IState
    {
        protected readonly EnemyShip EnemyShip;
        protected readonly BaseEnemyStateMachine BaseEnemyStateMachine;
        protected readonly EnemyTriggerColliderSettings TriggerColliderData;
        private readonly MovingStateSettings MovingStateSettings;

        public AIBaseState(EnemyShip enemyShip, BaseEnemyStateMachine baseEnemyStateMachine)
        {
            EnemyShip = enemyShip;
            BaseEnemyStateMachine = baseEnemyStateMachine;
            TriggerColliderData = EnemyShip.EnemyTriggerColliderSettings;
            MovingStateSettings = EnemyShip.StateSettings.MovingStateSettings;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
            if (IsTherePlayerNear())
            {
                Move(-EnemyShip.Rigidbody2D.transform.up);
                return;
            }
            
            if (IsThereBoundaryNear())
            {
                Move(EnemyShip.Rigidbody2D.transform.up);
                return;
            }

            Move(EnemyShip.Rigidbody2D.transform.right);
        }

        public virtual void OnTriggerEnter(Collider collider)
        {
        }

        public virtual void OnTriggerExit(Collider collider)
        {
        }

        private bool IsThereBoundaryNear()
        {
            var overlappedBoundColliders = GetPossibleBounds();

            return overlappedBoundColliders.Length > 0;
        }
        private bool IsTherePlayerNear()
        {
            return !(Vector3.Distance(EnemyShip.Target.transform.position, EnemyShip.transform.position) >
                     EnemyShip.StateSettings.MovingStateSettings.StoppingDistanceToPlayer);
        }
        private Collider2D[] GetPossibleBounds()
        {
            Vector3 colliderCenterInWorldSpace = TriggerColliderData.BoundaryCheckCollider.bounds.center;

            var overlappedBoundColliders = Physics2D.OverlapBoxAll(colliderCenterInWorldSpace,
                TriggerColliderData.BoundaryCheckColliderExtents,
                TriggerColliderData.BoundaryCheckCollider.transform.rotation.z,
                EnemyShip.StateSettings.LayerMasks.Boundary);
            return overlappedBoundColliders;
        }

        protected void Move(Vector2 direction)
        {
            EnemyShip.Rigidbody2D.AddForce(direction * MovingStateSettings.MovementSpeed);
        }
        
        protected void ResetVelocity()
        {
            EnemyShip.Rigidbody2D.velocity = Vector2.zero;
        }
        
        protected float GetViewAngle(Transform user, Transform target)
        {
            Vector3 targetDirection = target.position - user.position;

            float viewableAngle = Vector3.SignedAngle(targetDirection, user.forward, Vector3.up);
            return viewableAngle;
        }
    }
}