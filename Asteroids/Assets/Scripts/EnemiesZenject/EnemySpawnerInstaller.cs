using System;
using AsteroidZenject;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using EnemiesZenject;
using EnemiesZenject.AsteroidZenject;
using EnemyShipZenject;
using Entities;
using Entities.Core;
using Spawners;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemySpawnerInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidEntity _asteroidEntity;
        [SerializeField] private EnemyShip _enemyShip;

        public override void InstallBindings()
        {            
            Container.BindFactory<IEnemy, EnemyFactory>().FromFactory<CustomEnemyFactory>();

            Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyShipSpawner>().AsSingle();
            
            Container.BindFactory<Vector3, AsteroidFacade, AsteroidFacade.Factory>()
                .FromPoolableMemoryPool<Vector3, AsteroidFacade, AsteroidFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(10)
                    .FromSubContainerResolve()
                    .ByNewPrefabMethod(_asteroidEntity, InstallAsteroid));
            
            Container.BindFactory<EnemyShipFacade, EnemyShipFacade.Factory>()
                .FromPoolableMemoryPool<EnemyShipFacade, EnemyShipFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(10)
                    .FromSubContainerResolve()
                    .ByNewPrefabMethod(_enemyShip, InstallEnemyShip));

            Container.Bind<EntityRegistry>().AsSingle();
            
            GameplaySignalsInstaller.Install(Container);
        }

        private void InstallAsteroid(DiContainer subContainer)
        {
            subContainer.Bind<AsteroidFacade>().FromNewComponentOnRoot().AsSingle();
            subContainer.Bind<AsteroidDeathHandler>().AsSingle();
        }
        
        private void InstallEnemyShip(DiContainer subContainer)
        {
            subContainer.Bind<EnemyShipFacade>().FromNewComponentOnRoot().AsSingle();
        }

        class AsteroidFacadePool : MonoPoolableMemoryPool<Vector3, IMemoryPool, AsteroidFacade>
        {
        }

        class EnemyShipFacadePool : MonoPoolableMemoryPool<IMemoryPool, EnemyShipFacade>
        {
        }
    }
}