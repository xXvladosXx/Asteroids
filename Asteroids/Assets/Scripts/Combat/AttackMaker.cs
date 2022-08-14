using Interaction;
using UnityEngine;
using Utilities.Extensions;

namespace Combat
{
    public class AttackMaker : MonoBehaviour
    {
        private ObjectPicker _objectPicker;
        
        private bool _readyToAttack = true;
        
        public void Init(ObjectPicker objectPicker)
        {
            _objectPicker = objectPicker;
        }
        public bool CanMakeFire() => _readyToAttack;

        public void Fire(Transform transform)
        {
            _readyToAttack = false;
            
            var bullet = Instantiate(_objectPicker.CurrentProjectile, transform.position, transform.rotation);
            bullet.Fire(transform.up);    
            
            this.CallWithDelay(ResetShot, _objectPicker.CurrentProjectile.ProjectileData.TimeBetweenShooting);
        }

        private void ResetShot()
        {
            _readyToAttack = true;
        }
    }
}