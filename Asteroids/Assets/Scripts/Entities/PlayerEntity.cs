using System;
using Data.Player;
using Entities.Core;
using UnityEngine;
using Utilities.Input;

namespace Entities
{
    [RequireComponent(typeof(PlayerInput),
        typeof(Rigidbody2D))]
    public class PlayerEntity : ShipEntity
    {
        [field: SerializeField] public PlayerData PlayerData { get; private set; }
        
        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody2D;
        
        private bool _moving;
        private float _movementDirection;
        protected override void Awake()
        {
            base.Awake();

            _playerInput = GetComponent<PlayerInput>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public override void Die()
        {
            Destroy(gameObject);    
        }
        
        private void Update()
        {
            ReadMovement();
            Rotate();

            if (_playerInput.InputActions.PlayerActions.Fire.WasPressedThisFrame())
            {
                Shoot();
            }
        }

        private void ReadMovement()
        {
            _moving = _playerInput.InputActions.PlayerActions.Movement.ReadValue<Vector2>().y > 0;
        }

        private void Rotate()
        {
            if (_playerInput.InputActions.PlayerActions.Movement.ReadValue<Vector2>().x > 0)
            {
                _movementDirection = -1;
            }
            else if (_playerInput.InputActions.PlayerActions.Movement.ReadValue<Vector2>().x < 0)
            {
                _movementDirection = 1;
            }
            else
            {
                _movementDirection = 0;
            }
        }

        private void FixedUpdate()
        {
            if (_moving)
            {
                _rigidbody2D.AddForce(transform.up * PlayerData.MovementSpeed);
            }

            if (_movementDirection != 0)
            {
                _rigidbody2D.AddTorque(_movementDirection * PlayerData.RotationSpeed);
            }
        }

        private void Shoot()
        {
            var bullet = Instantiate(CombatData.BulletPrefab, transform.position, transform.rotation);
            bullet.Fire(transform.up);
        }
        
        
    }
}
