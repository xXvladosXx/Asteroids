using System;
using AudioSystem;
using Combat.Core;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Interaction;
using ObjectPoolers;
using UnityEngine;
using Utilities.Extensions;
using Zenject;

namespace Combat
{
    public class AttackMaker : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _misslePrefab;
        
        private ObjectPicker _objectPicker;
        private ParticleSystem _missleObject;
        private OrdinaryProjectile.Factory _projectileFactory;
        
        private bool _readyToAttack = true;

        [Inject]
        public void Construct(OrdinaryProjectile.Factory projectileFactory)
        {
            _projectileFactory = projectileFactory;
        }
        
        public void Init(ObjectPicker objectPicker)
        {
            _objectPicker = objectPicker;
            
            _missleObject = Instantiate(_misslePrefab);
            _missleObject.Stop();
        }
        public bool CanMakeFire() => _readyToAttack;

        public void Fire(HitData hitData)
        {
            _readyToAttack = false;
            var bullet = _projectileFactory.Create();
            AudioManager.Instance.PlayEffectSound(bullet.ProjectileData.AudioClip);

            var position = hitData.AttackApplier.User.position;
            var rotation = hitData.AttackApplier.User.rotation;

            _missleObject.transform.position = position;
            _missleObject.transform.rotation = rotation;
            _missleObject.Play();
            
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.ApplyAttack(hitData);    
            
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
}