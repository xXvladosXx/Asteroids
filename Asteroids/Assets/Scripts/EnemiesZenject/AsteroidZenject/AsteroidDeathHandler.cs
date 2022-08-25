using Entities;
using Zenject;

namespace AsteroidZenject
{
    public class AsteroidDeathHandler
    {
        private readonly AsteroidFacade _asteroidFacade;
        private readonly SignalBus _signalBus;
        private readonly AsteroidEntity _asteroidEntity;

        public AsteroidDeathHandler(AsteroidFacade asteroidFacade,
            SignalBus signalBus,
            AsteroidEntity asteroidEntity)
        {
            _asteroidFacade = asteroidFacade;
            _signalBus = signalBus;
            _asteroidEntity = asteroidEntity;
        }
        
        public void Die()
        {
            _signalBus.Fire(new AsteroidKilledSignal(_asteroidEntity));

            _asteroidFacade.Dispose();
        }
    }
}