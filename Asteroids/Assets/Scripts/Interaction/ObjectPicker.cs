using System;
using BonusesSystem;
using Combat.Projectiles.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Interaction
{
    [Serializable]
    public class ObjectPicker
    {
        [field: SerializeField] public Projectile CurrentProjectile { get; private set; }
        [field: SerializeField] public Projectile DefaultProjectile { get; private set; }
        
        private BonusHandler _bonusHandler;

        public void Init(BonusHandler bonusHandler)
        {
            _bonusHandler = bonusHandler;
        }
        
        public void PickupObject(PickableObject pickableObject)
        {
            Debug.Log(_bonusHandler);

            switch (pickableObject.Pickable)
            {
                case TimeableBonus timeableBonus:
                    _bonusHandler.AddBonus(timeableBonus);
                    Object.Destroy(pickableObject.gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}