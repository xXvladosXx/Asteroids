using System;
using AsteroidZenject;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Entities;
using Spawners;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Installers
{
    public class AsteroidSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidEntity _asteroidEntity;

        public override void InstallBindings()
        {            
            Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();
            
            Container.BindFactory<Vector3, AsteroidFacade, AsteroidFacade.Factory>()
                .FromPoolableMemoryPool<Vector3, AsteroidFacade, AsteroidFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(10)
                    .FromSubContainerResolve()
                    .ByNewPrefabMethod(_asteroidEntity, InstallEnemy));
            
            Container.Bind<AsteroidRegistry>().AsSingle();

            GameplaySignalsInstaller.Install(Container);
        }

        private void InstallEnemy(DiContainer subContainer)
        {
            subContainer.Bind<AsteroidFacade>().FromNewComponentOnRoot().AsSingle();
            subContainer.Bind<AsteroidDeathHandler>().AsSingle();
        }

        class AsteroidFacadePool : MonoPoolableMemoryPool<Vector3, IMemoryPool, AsteroidFacade>
        {
        }

        
    }
}