using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace StatsSystem.Core
{
    [CreateAssetMenu(menuName = "Stats/StatsData")]
    public class StatsData : SerializedScriptableObject
    {
        public Dictionary<Stats, float> Stats;

        public float GetStat(Stats stat)
        {
            Stats.TryGetValue(stat, out var value);

            return value;
        }
    }

    public enum Stats
    {
        Health,
        Damage
    }
}