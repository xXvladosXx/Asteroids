namespace BonusesSystem
{
    public class ProjectileLineBonus : IBonus
    {
        public ProjectileLineBonus(float bonus) => Value = bonus;
        public float Value { get; }
    }
}