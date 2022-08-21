using Spawners;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class AsteroidSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidSpawner _asteroidSpawner;

        public override void InstallBindings()
        {            
            Container.Bind<AsteroidSpawner>().FromInstance(_asteroidSpawner).AsSingle();
        }
    }
}