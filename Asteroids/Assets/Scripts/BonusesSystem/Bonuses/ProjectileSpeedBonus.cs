namespace BonusesSystem
{
    public class ProjectileSpeedBonus: IBonus
    {
        public ProjectileSpeedBonus(float bonus) => Value = bonus;
        public float Value { get; }
    }
}