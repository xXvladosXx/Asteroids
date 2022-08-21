using UnityEngine;

namespace Combat.Core
{
    public class HitData
    {
        public float Damage { get; set; }
        public Transform Transform { get; set; }
        public IHitDetector HitDetector { get; set; }
        public IHurtbox Hurtbox { get; set; }
    }
}