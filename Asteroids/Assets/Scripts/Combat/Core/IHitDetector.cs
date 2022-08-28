namespace Combat.Core
{
    public interface IHitDetector
    {
        public IDamageReceiver HitResponder { get; set; }
        public void CheckHit();
    }
}