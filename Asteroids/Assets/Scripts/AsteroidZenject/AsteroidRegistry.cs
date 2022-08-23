using System.Collections.Generic;

namespace AsteroidZenject
{
    public class AsteroidRegistry
    {
        readonly List<AsteroidFacade> _asteroidFacades = new List<AsteroidFacade>();

        public void AddEnemy(AsteroidFacade asteroidFacade)
        {
            _asteroidFacades.Add(asteroidFacade);
        }

        public void RemoveEnemy(AsteroidFacade asteroidFacade)
        {
            _asteroidFacades.Remove(asteroidFacade);
        }
    }
}