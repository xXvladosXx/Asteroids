using System;
using AsteroidsZenject;
using AsteroidsZenject.AsteroidZenject;
using AsteroidsZenject.EnemyShipZenject;
using Combat.Projectiles;
using Combat.Projectiles.Core;
using Controllers;
using Entities;
using Entities.Core;
using Spawners;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidEntity _asteroidEntity;
        [SerializeField] private EnemyShip _enemyShip;

        public override void InstallBindings()
        {            
            InstallAsteroids();
            InstallEnemyShips();

            GameplaySignalsInstaller.Install(Container);
        }

        private void InstallEnemyShips()
        {
            Container.BindInterfacesAndSelfTo<EnemiesController>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyShipSpawner>().AsSingle();
            Container.BindFactory<Vector3, EnemyShipFacade, EnemyShipFacade.Factory>()
                .FromPoolableMemoryPool<Vector3, EnemyShipFacade, EnemyShipFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(100)
                    .FromSubContainerResolve()
                    .ByNewPrefabMethod(_enemyShip, InstallEnemyShip)
                    .UnderTransformGroup("Enemy Ships"));
            Container.Bind<EnemyShipRegistry>().AsSingle();
        }

        private void InstallAsteroids()
        {
            Container.BindInterfacesAndSelfTo<AsteroidSpawner>().AsSingle();
            Container.BindFactory<float, Vector3, AsteroidFacade, AsteroidFacade.Factory>()
                .FromPoolableMemoryPool<float, Vector3, AsteroidFacade, AsteroidFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(100)
                    .FromSubContainerResolve()
                    .ByNewPrefabMethod(_asteroidEntity, InstallAsteroid)
                    .UnderTransformGroup("Asteroids"));
            Container.Bind<AsteroidRegistry>().AsSingle();
        }

        private void InstallAsteroid(DiContainer subContainer)
        {
            subContainer.Bind<AsteroidFacade>().FromNewComponentOnRoot().AsSingle();
            subContainer.Bind<AsteroidDeathHandler>().AsSingle();
        }
        
        private void InstallEnemyShip(DiContainer subContainer)
        {
            subContainer.Bind<EnemyShipFacade>().FromNewComponentOnRoot().AsSingle();
            subContainer.Bind<EnemyShip>().FromComponentOnRoot().AsSingle();
            subContainer.BindInterfacesAndSelfTo<EnemyShipDeathHandler>().AsSingle();
        }

        class AsteroidFacadePool : MonoPoolableMemoryPool<float, Vector3, IMemoryPool, AsteroidFacade>
        {
        }

        class EnemyShipFacadePool : MonoPoolableMemoryPool<Vector3, IMemoryPool, EnemyShipFacade>
        {
        }
    }
}