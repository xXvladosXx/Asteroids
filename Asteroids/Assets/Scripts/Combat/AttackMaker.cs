using AudioSystem;
using Combat.Projectiles.Core;
using Interaction;
using ObjectPoolers;
using UnityEngine;
using Utilities.Extensions;

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

        public void Fire(Transform transform)
        {
            _readyToAttack = false;
            var bullet = ProjectilePool.Instance.GetPrefab();
            AudioManager.Instance.PlayEffectSound(bullet.ProjectileData.AudioClip);

            _missleObject.transform.position = transform.position;
            _missleObject.transform.rotation = transform.rotation;
            _missleObject.Play();
            
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.Fire(transform.up);    
            
            this.CallWithDelay(ResetShot, _objectPicker.CurrentProjectile.ProjectileData.TimeBetweenShooting);
        }

        private void ResetShot()
        {
            _readyToAttack = true;
        }
    }
}