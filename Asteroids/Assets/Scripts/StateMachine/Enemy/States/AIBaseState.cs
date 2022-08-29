using Data.EnemyShip;
using Data.EnemyShip.StatesSettings;
using Entities;
using StateMachine.Core;
using UnityEngine;

namespace StateMachine.Enemy.States
{
    public abstract class AIBaseState : IState, ITargetFinder
    {
        protected readonly EnemyShip EnemyShip;
        protected readonly BaseEnemyStateMachine BaseEnemyStateMachine;
        protected readonly EnemyTriggerColliderSettings TriggerColliderData;
        
        private readonly MovingStateSettings _movingStateSettings;
        
        public AIBaseState(EnemyShip enemyShip, BaseEnemyStateMachine baseEnemyStateMachine)
        {
            EnemyShip = enemyShip;
            BaseEnemyStateMachine = baseEnemyStateMachine;
            TriggerColliderData = EnemyShip.EnemyTriggerColliderSettings;
            _movingStateSettings = EnemyShip.EnemyShipData.StateSettings.MovingStateSettings;
        }

        public virtual void Enter()
        {
            BaseEnemyStateMachine.EnemyStateReusableData.RotationSpeed = _movingStateSettings.RotationSpeed;
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            FindTarget(FindAsteroids());
        }

        public virtual void FixedUpdate()
        {
            if (IsThereAsteroidNear())
            {
                AvoidAsteroid();

                return;
            }
            
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

        public virtual void FindTarget(Collider2D[] colliders)
        {
            Transform possibleTarget = null;
                
            foreach (var possibleAsteroid in colliders)
            {
                possibleTarget = possibleAsteroid.transform;

                if (Vector2.Distance(possibleTarget.transform.position, EnemyShip.transform.position) >
                    Vector2.Distance(possibleAsteroid.transform.position, EnemyShip.transform.position))
                {
                    possibleTarget = possibleAsteroid.transform;
                }
                    
                BaseEnemyStateMachine.EnemyStateReusableData.AsteroidLastMoveDirection = 
                    (possibleTarget.transform.position - BaseEnemyStateMachine.EnemyStateReusableData.AsteroidPreviousPosition).normalized;
                BaseEnemyStateMachine.EnemyStateReusableData.AsteroidPreviousPosition = possibleTarget.transform.position;
            }

            RotateTowardsTargetPosition(possibleTarget);
        }

        public Collider2D[] PossibleTargets(LayerMask layerMask, Vector2 centre, Vector2 size, float angle)
        {
            Vector3 centerInWorldSpace = centre;

            var possibleTargets = Physics2D.OverlapBoxAll(
                centerInWorldSpace, size, angle, layerMask);
            
            return possibleTargets;
        }

        private void Move(Vector2 direction)
        {
            EnemyShip.Rigidbody2D.AddForce(direction * _movingStateSettings.MovementSpeed);
        }
        
        private bool IsTherePlayerNear()
        {
            return !(Vector3.Distance(EnemyShip.Target.transform.position, EnemyShip.transform.position) >
                     EnemyShip.EnemyShipData.StateSettings.MovingStateSettings.StoppingDistanceToPlayer);
        }
        
        private bool IsThereBoundaryNear() => FindBoundaries().Length > 0;

        private Collider2D[] FindBoundaries() => PossibleTargets(EnemyShip.LayerMasks.Boundary, 
                TriggerColliderData.BoundaryCheckCollider.bounds.center,
                TriggerColliderData.BoundaryCheckColliderExtents,
                TriggerColliderData.BoundaryCheckCollider.transform.rotation.z);

        private Collider2D[] FindAsteroids() => PossibleTargets(EnemyShip.LayerMasks.Asteroid,
                TriggerColliderData.AsteroidCheckCollider.bounds.center,
                TriggerColliderData.AsteroidCheckColliderExtents,
                TriggerColliderData.AsteroidCheckCollider.transform.rotation.z);
        protected bool IsThereAsteroidNear() => FindAsteroids().Length > 0;
        
        private void AvoidAsteroid()
        {
            if (BaseEnemyStateMachine.EnemyStateReusableData.AsteroidLastMoveDirection.x < 0)
            {
                Move(EnemyShip.Rigidbody2D.transform.right);
            }
            else
            {
                Move(-EnemyShip.Rigidbody2D.transform.right);
            }

            if (BaseEnemyStateMachine.EnemyStateReusableData.AsteroidLastMoveDirection.y < 0)
            {
                Move(EnemyShip.Rigidbody2D.transform.up);
            }
            else
            {
                Move(-EnemyShip.Rigidbody2D.transform.up);
            }
        }
        
        protected void RotateTowardsTargetPosition(Transform target)
        {
            if(target == null) return;
            
            Vector3 position = EnemyShip.Rigidbody2D.transform.position;
            Vector3 targetPosition = target.transform.position;

            float angle = Mathf.Atan2(target.position.y - position.y, targetPosition.x - position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            EnemyShip.Rigidbody2D.transform.rotation = Quaternion.RotateTowards(EnemyShip.Rigidbody2D.transform.rotation,
                targetRotation, _movingStateSettings.RotationSpeed * Time.deltaTime);
        }
    }
}