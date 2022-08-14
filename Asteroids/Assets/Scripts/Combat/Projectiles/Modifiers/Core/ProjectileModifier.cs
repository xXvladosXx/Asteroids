using UnityEngine;

namespace Combat.Projectiles.Modifiers
{
    [CreateAssetMenu(menuName = "Projectile/Modifier")]
    public abstract class ProjectileModifier : ScriptableObject
    {
        public abstract void ApplyModifier(ModifierData modifierData);
    }
}