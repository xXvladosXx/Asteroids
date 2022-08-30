namespace BonusesSystem
{
    public class DamageBonus : IBonus
    {
        public DamageBonus(float bonus) => Value = bonus;
        public float Value { get; }
    }
}