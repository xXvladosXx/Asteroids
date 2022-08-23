using Entities;

namespace AsteroidZenject
{
    public struct AsteroidKilledSignal
    {
        public readonly AsteroidEntity AsteroidEntity;
        
        public AsteroidKilledSignal(AsteroidEntity asteroidEntity)
        {
            AsteroidEntity = asteroidEntity;
        }
    }
}