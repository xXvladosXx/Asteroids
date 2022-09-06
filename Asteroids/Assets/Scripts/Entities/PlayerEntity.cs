using System;
using BonusesSystem;
using Camera;
using Combat;
using Combat.Core;
using Data.Player;
using Entities.Core;
using Interaction;
using StatsSystem;
using StatsSystem.Core;
using UnityEngine;
using Utilities.Input;
using Zenject;

namespace Entities
{
    [RequireComponent(typeof(PlayerInput),
        typeof(Rigidbody2D))]
    public class PlayerEntity : ShipEntity, IBonusUser
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private PlayerInput _playerInput;
        private CameraShaker _cameraShaker;
        public BonusHandler BonusHandler { get; set; }

        private bool _moving;
        private float _movementDirection;
        private PlayerSettings _playerSettings;
        public BonusFinder BonusFinder { get; set; }

        public override event Action<IAttackApplier> OnDied;

        [Inject]
        public void Construct(PlayerInput playerInput,
            Heath heath, CameraShaker cameraShaker,
            BonusHandler bonusHandler,
            PlayerSettings playerSettings)
        {
            Heath = heath;
            _playerInput = playerInput;
            _cameraShaker = cameraShaker;
            BonusHandler = bonusHandler;
            _playerSettings = playerSettings;
            
            BonusFinder = new BonusFinder(bonusHandler);
            ObjectPicker.Init(bonusHandler);
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
            Heath.IncreaseHealth(BonusFinder.GetBonus(Stat.HealthRegeneration));
            
            ReadMovement();
            Rotate();
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
            if (_playerInput.InputActions.PlayerActions.Fire.IsPressed())
            {
                ApplyAttack(new HitData
                {
                    Damage = StatsData.GetStat(Stats.Damage) + BonusFinder.GetBonus(Stat.Damage),
                    Hurtbox = this,
                    AttackApplier = this,
                    BonusUser = this
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
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.TryGetComponent(out PickableObject pickableObject))
            {
                ObjectPicker.PickupObject(pickableObject);
            }
        }
    }
}
