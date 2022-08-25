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
        
        private bool _readyToAttack = true;
        
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
            var bullet = ProjectilePool.Instance.GetPrefab();
            AudioManager.Instance.PlayEffectSound(bullet.ProjectileData.AudioClip);

            _missleObject.transform.position = hitData.DamageApplier.position;
            _missleObject.transform.rotation = hitData.DamageApplier.rotation;
            _missleObject.Play();
            
            bullet.transform.position = hitData.DamageApplier.position;
            bullet.transform.rotation = hitData.DamageApplier.rotation;
            bullet.ApplyAttack(hitData);    
            
            this.CallWithDelay(ResetShot, _objectPicker.CurrentProjectile.ProjectileData.TimeBetweenShooting);
        }

        private void ResetShot()
        {
            _readyToAttack = true;
        }
    }
}