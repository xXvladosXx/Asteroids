using Combat.Projectiles.Core;
using UnityEngine;

namespace Combat.Projectiles.Modifiers
{
    [CreateAssetMenu(menuName = "Projectile/SpreadModifier")]
    public class SpreadModifier : ProjectileModifier
    {
        [field: SerializeField] public int SpreadAmount { get; private set; }
        [field: SerializeField] public float StartAngle { get; private set; } = 90;
        [field: SerializeField] public float EndAngle { get; private set; } = 270;
        [field: SerializeField] public Projectile Projectile { get; private set; }
        
        public override void ApplyModifier(ModifierData modifierData)
        {
            float angleStep = 360f / SpreadAmount;
            float angle = 0f;

            
            for (int i = 0; i < SpreadAmount ; i++)
            {
                float spreadDirX = modifierData.Transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
                float spreadDirY = modifierData.Transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

                Vector3 spreadMoveVector = new Vector3(spreadDirX, spreadDirY);
                Vector2 spreadDir = (spreadMoveVector - modifierData.Transform.position).normalized;
                
                var projectile = Instantiate(Projectile);
                projectile.transform.position = modifierData.Transform.position;
                projectile.transform.rotation = modifierData.Transform.rotation;
                
                angle += angleStep;
            }
        }
    }
}