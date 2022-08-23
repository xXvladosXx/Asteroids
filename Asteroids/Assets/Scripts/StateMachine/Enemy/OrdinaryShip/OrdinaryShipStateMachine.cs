namespace StateMachine.Enemy.OrdinaryShip
{
    public class OrdinaryShipStateMachine : Core.StateMachine
    {
        private readonly Entities.OrdinaryShip _ordinaryShip;
        
        public OrdinaryShipStateMachine(Entities.OrdinaryShip ordinaryShip)
        {
            _ordinaryShip = ordinaryShip;
        }
    }
}