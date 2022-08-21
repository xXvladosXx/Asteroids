namespace Combat.Core
{
    public interface IHitDetector
    {
        public IDamagable HitResponder { get; set; }
        public void CheckHit();
    }
}