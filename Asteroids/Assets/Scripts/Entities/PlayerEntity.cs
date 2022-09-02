using System;
using Camera;
using Combat;
using Combat.Core;
using Core;
using Data.Player;
using Entities.Core;
using Interaction;
using Interaction.Weapon;
using StatsSystem;
using StatsSystem.Core;
using UnityEngine;
using Utilities.Input;
using Zenject;

namespace Entities
{
    [RequireComponent(typeof(PlayerInput),
        typeof(Rigidbody2D))]
    public class PlayerEntity : ShipEntity
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private PlayerInput _playerInput;
        private CameraShaker _cameraShaker;
        
        private bool _moving;
        private float _movementDirection;
<<<<<<< Updated upstream
=======
        private PlayerSettingsSO _playerSettings;
        public BonusFinder BonusFinder { get; set; }
>>>>>>> Stashed changes

        public override event Action<IAttackApplier> OnDied;

        [Inject]
        public void Construct(PlayerInput playerInput,
<<<<<<< Updated upstream
            Heath heath, CameraShaker cameraShaker)
=======
            Heath heath, CameraShaker cameraShaker,
            BonusHandler bonusHandler,
            PlayerSettingsSO playerSettingsSo)
>>>>>>> Stashed changes
        {
            Heath = heath;
            _playerInput = playerInput;
            _cameraShaker = cameraShaker;
<<<<<<< Updated upstream
=======
            BonusHandler = bonusHandler;
            _playerSettings = playerSettingsSo;
            BonusFinder = new BonusFinder(bonusHandler);
            ObjectPicker.Init(bonusHandler);
>>>>>>> Stashed changes
        }
        
        public override void Die(IAttackApplier attackApplier)
        {
            gameObject.SetActive(false);
            
            _cameraShaker.StartShaking(_cameraShaker.CameraShakerData.PlayerDeathTime, 
                _cameraShaker.CameraShakerData.PlayerDeathMagnitude);
            
            OnDied?.Invoke(attackApplier);
        }

        public override void ReceiveDamage(HitData hitData)
        {
            base.ReceiveDamage(hitData);
            _cameraShaker.StartShaking(_cameraShaker.CameraShakerData.PlayerDamageTime, 
                _cameraShaker.CameraShakerData.PlayerDamageMagnitude);
        }

        private void Update()
        {
            ReadMovement();
            Rotate();
<<<<<<< Updated upstream

=======
            MakeAttack();
        }
        private void FixedUpdate()
        {
            if (_moving)
            {
                _rigidbody2D.AddForce(transform.up * _playerSettings.MovementSpeed);
            }

            if (_movementDirection != 0)
            {
                _rigidbody2D.AddTorque(_movementDirection * _playerSettings.RotationSpeed);
            }
        }
        private void MakeAttack()
        {
>>>>>>> Stashed changes
            if (_playerInput.InputActions.PlayerActions.Fire.IsPressed())
            {
                ApplyAttack(new HitData
                {
                    Damage = StatsData.GetStat(Stats.Damage),
                    Hurtbox = this,
                    AttackApplier = this
                }, null);
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
                _rigidbody2D.AddForce(transform.up * PlayerSettings.MovementSpeed);
            }

            if (_movementDirection != 0)
            {
                _rigidbody2D.AddTorque(_movementDirection * PlayerSettings.RotationSpeed);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.TryGetComponent(out PickableObject pickableObject))
            {
                ObjectPicker.PickupObject(pickableObject);
            }
        }
    }
}
