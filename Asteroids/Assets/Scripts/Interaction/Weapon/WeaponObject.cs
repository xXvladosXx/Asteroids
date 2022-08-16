using Combat;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using UnityEngine;

namespace Interaction.Weapon
{
    public class WeaponObject : PickableObject
    {
        [field: SerializeField] public Projectile Projectile { get; private set; }
    }
}