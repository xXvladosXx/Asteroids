namespace BonusesSystem
{
    public interface IBonus
    {
        float Value { get; }
    }
    
    public enum Stat
    {
        ProjectileLine,
        Damage,
        HealthRegeneration,
        ProjectileSpeed,
        FrameRate
    }
}