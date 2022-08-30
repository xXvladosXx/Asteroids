using BonusesSystem;
using UnityEngine;

namespace Combat.Core
{
    public class HitData
    {
        public float Damage { get; set; }
        public IAttackApplier AttackApplier { get; set; }
        public IBonusUser BonusUser { get; set; }
        public IHitDetector HitDetector { get; set; }
        public IHurtbox Hurtbox { get; set; }
    }
}