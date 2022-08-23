using Entities;
using StatsSystem;
using StatsSystem.Core;
using UnityEngine;
using Zenject;
using Zenject.Asteroids;

namespace Installers
{
    public class AsteroidInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidEntity _asteroidEntity;

        public override void InstallBindings()
        {
            Container.Bind<Heath>().FromSubContainerResolve().ByMethod(InstallAsteroidHealth).AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidEntity>().FromInstance(_asteroidEntity).AsSingle();
        }

        private void InstallAsteroidHealth(DiContainer subContainer)
        {
            subContainer.Bind<Heath>().AsSingle();
            subContainer.BindInstance(_asteroidEntity.StatsData.GetStat(Stats.Health)).AsSingle();
        }
    }
}