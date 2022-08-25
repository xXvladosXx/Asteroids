using Combat.Core;
using Data.EnemyShip;
using EnemyShipZenject;
using Entities.Core;
using Pathfinding;
using StateMachine.Core;
using StatsSystem.Core;
using UnityEngine;

namespace StateMachine.Enemy.BaseStates
{
    public class AIAttackState : AIBaseState
    {
        private readonly AttackStateSettings _attackStateSettings;
        
        private Vector3 _asteroidPreviousPosition;
        private Vector3 _asteroidLastMoveDirection;
        
        public AIAttackState(EnemyShip enemyShip, BaseEnemyStateMachine baseEnemyStateMachine) : base(enemyShip, baseEnemyStateMachine)
        {
            _attackStateSettings = EnemyShip.StateSettings.AttackStateSettings;
        }

        public override void Update()
        {
            EnemyShip.ApplyAttack(new HitData
            {
                Damage = EnemyShip.StatsData.GetStat(Stats.Damage),
                Hurtbox = EnemyShip,
                DamageApplier = EnemyShip.transform 
            });

            FindTarget();
        }

        public override void FixedUpdate()
        {
            if (GetPossibleAsteroids().Length > 0)
            {
                AvoidAsteroid();

                return;
            }
            
            base.FixedUpdate();
        }

        private void AvoidAsteroid()
        {
            if (_asteroidLastMoveDirection.x < 0)
            {
                Move(EnemyShip.Rigidbody2D.transform.right);
            }
            else
            {
                Move(-EnemyShip.Rigidbody2D.transform.right);
            }

            if (_asteroidLastMoveDirection.y < 0)
            {
                Move(EnemyShip.Rigidbody2D.transform.up);
            }
            else
            {
                Move(-EnemyShip.Rigidbody2D.transform.up);
            }
        }

        private void FindTarget()
        {
            if (IsThereAsteroidNear())
            {
                Transform possibleTarget = null;
                
                foreach (var possibleAsteroid in GetPossibleAsteroids())
                {
                    if (possibleTarget == null)
                    {
                        possibleTarget = possibleAsteroid.transform;
                    }

                    if (Vector2.Distance(possibleTarget.transform.position, EnemyShip.transform.position) >
                        Vector2.Distance(possibleAsteroid.transform.position, EnemyShip.transform.position))
                    {
                        possibleTarget = possibleAsteroid.transform;
                    }
                    
                    _asteroidLastMoveDirection = (possibleTarget.transform.position - _asteroidPreviousPosition).normalized;
                    _asteroidPreviousPosition = possibleTarget.transform.position;
                }

                RotateTowardsTargetPosition(possibleTarget);
                return;
            }
            
            RotateTowardsTargetPosition(EnemyShip.Target);
        }

        private void RotateTowardsTargetPosition(Transform target)
        {
            if(target == null) return;
            
            Vector3 position = EnemyShip.Rigidbody2D.transform.position;
            Vector3 targetPosition = target.transform.position;

            float angle = Mathf.Atan2(target.position.y - position.y, targetPosition.x - position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            EnemyShip.Rigidbody2D.transform.rotation = Quaternion.RotateTowards(EnemyShip.Rigidbody2D.transform.rotation,
                targetRotation, _attackStateSettings.RotationSpeed * Time.deltaTime);
        }
        
        private Collider2D[] GetPossibleAsteroids()
        {
            Vector3 colliderCenterInWorldSpace = TriggerColliderData.AsteroidCheckCollider.bounds.center;

            var overlappedBoundColliders = Physics2D.OverlapBoxAll(colliderCenterInWorldSpace,
                TriggerColliderData.AsteroidCheckColliderExtents,
                TriggerColliderData.AsteroidCheckCollider.transform.rotation.z,
                EnemyShip.StateSettings.LayerMasks.Asteroid);
            
            return overlappedBoundColliders;
        }

        private bool IsThereAsteroidNear() => GetPossibleAsteroids().Length > 0;
    }
}