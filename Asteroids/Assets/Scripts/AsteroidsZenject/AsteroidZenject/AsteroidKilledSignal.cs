using Combat.Core;
using Entities;

namespace AsteroidZenject
{
    public struct AsteroidKilledSignal 
    {
        public readonly AsteroidEntity AsteroidEntity;
        public readonly IAttackApplier Destroyer;
        
        public AsteroidKilledSignal(AsteroidEntity asteroidEntity, IAttackApplier destroyer)
        {
            AsteroidEntity = asteroidEntity;
            Destroyer = destroyer;
        }
    }
}