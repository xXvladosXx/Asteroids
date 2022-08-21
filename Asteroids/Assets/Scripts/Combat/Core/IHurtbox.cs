namespace Combat.Core
{
    public interface IHurtbox
    {
        IDamagable Damagable { get; set; }
    }
}