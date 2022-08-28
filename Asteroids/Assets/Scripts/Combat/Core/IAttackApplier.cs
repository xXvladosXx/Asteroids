using Core;
using UnityEngine;

namespace Combat.Core
{
    public interface IAttackApplier
    {
        Transform User { get; }
        IScoreCollector ScoreCollector { get; }
        public void ApplyAttack(HitData hitData, IDamageReceiver damageReceiver);
    }
}