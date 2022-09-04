using System;
using AudioSystem;
using BonusesSystem;
using Combat.Core;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Combat.Projectiles.Modifiers;
using Interaction;
using UnityEngine;
using Utilities.Extensions;
using Zenject;

namespace Combat
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _misslePrefab;

        private ObjectPicker _objectPicker;
        private ParticleSystem _missleObject;
        private OrdinaryProjectile.Factory _projectileFactory;
        private AudioManager _audioManager;

        private bool _readyToAttack = true;
        public bool CanMakeFire() => _readyToAttack;

        [Inject]
        public void Construct(OrdinaryProjectile.Factory projectileFactory,
            AudioManager audioManager)
        {
            _projectileFactory = projectileFactory;
            _audioManager = audioManager;
        }

        public void Init(ObjectPicker objectPicker)
        {
            _objectPicker = objectPicker;

            _missleObject = Instantiate(_misslePrefab);
            _missleObject.Stop();
        }

        public void Fire(HitData hitData, Vector3 position)
        {
            ProjectileModifiersData projectileModifiersData = new ProjectileModifiersData();
            
            _readyToAttack = false;
            var bullet = _projectileFactory.Create();
            _audioManager.PlayEffectSound(bullet.ProjectileData.AudioClip);

            var rotation = hitData.AttackApplier.User.rotation;

            _missleObject.transform.position = position;
            _missleObject.transform.rotation = rotation;
            _missleObject.Play();

            bullet.transform.rotation = rotation;
            bullet.transform.position = position;

            if (hitData.BonusUser != null)
            {
                var lines = (int) hitData.BonusUser.BonusFinder.GetBonus(Stat.ProjectileLine);
                projectileModifiersData.AdditionalSpeed = hitData.BonusUser.BonusFinder.GetBonus(Stat.ProjectileSpeed);
                    
                for (int i = 1; i < lines + 1; i++)
                {
                    var bulletLine = _projectileFactory.Create();
                    _audioManager.PlayEffectSound(bullet.ProjectileData.AudioClip);
                    bulletLine.transform.rotation = rotation;
                    
                    if (i % 2 == 0)
                    {
                        bulletLine.transform.position = position + (hitData.AttackApplier.User.right * (i * .1f));
                    }
                    else
                    {
                        bulletLine.transform.position = position + (-hitData.AttackApplier.User.right * (i * .1f));
                    }
                   
                    bulletLine.ApplyAttack(hitData, projectileModifiersData);
                }
            }

            bullet.ApplyAttack(hitData, projectileModifiersData);

            this.CallWithDelay(ResetShot, _objectPicker.CurrentProjectile.ProjectileData.TimeBetweenShooting);
        }

        private void ResetShot()
        {
            _readyToAttack = true;
        }

        private void OnEnable()
        {
            _readyToAttack = true;
        }
    }

    public class ProjectileModifiersData
    {
        public float AdditionalSpeed { get; set; }
    }
}