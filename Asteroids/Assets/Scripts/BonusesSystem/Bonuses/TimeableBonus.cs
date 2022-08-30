using Interaction;
using UnityEngine;

namespace BonusesSystem
{
    [CreateAssetMenu (menuName = "SkillSystem/Bonus/TimeableBonus")]
    public class TimeableBonus : ScriptableObject, ITimeable, IPickable
    {
        [field: SerializeField] public int Id { get; private set; }

        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public bool Positive { get; private set; }
        [field: SerializeField] public Stat Stat { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}