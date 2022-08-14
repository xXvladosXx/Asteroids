using System;
using Combat;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Interaction.Weapon;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Interaction
{
    [Serializable]
    public class ObjectPicker
    {
        [field: SerializeField] public Projectile CurrentProjectile { get; private set; }
        [field: SerializeField] public Projectile DefaultProjectile { get; private set; }

        public void PickupObject(PickableObject pickableObject)
        {
            switch (pickableObject)
            {
                case WeaponObject weaponObject:
                    ChangeWeapon(weaponObject);
                    Object.Destroy(weaponObject.gameObject);
                    break;
            }
        }
        
        public void ChangeWeapon(WeaponObject weapon)
        {
            Debug.Log("Picked");
            CurrentProjectile = weapon.Projectile;
        }
    }
}