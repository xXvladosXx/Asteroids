using Controllers;
using Entities;
using Interaction;
using StatsSystem;
using StatsSystem.Core;
using UI.Enemy;
using UnityEngine;
using Zenject;

namespace AsteroidsZenject.EnemyShipZenject
{
    public class EnemyShipInstaller : MonoInstaller
    {
        [SerializeField] private EnemyShip _enemyEntity;
        [SerializeField] private HealthBar _enemyHealthUI;

        public override void InstallBindings()
        {
            Container.Bind<Heath>().FromSubContainerResolve().ByMethod(InstallEnemyShipHealth).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyShip>().FromInstance(_enemyEntity).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyHealthController>().AsSingle();
        }

        private void InstallEnemyShipHealth(DiContainer subContainer)
        {
            subContainer.Bind<Heath>().AsSingle();

            subContainer.BindInstance(_enemyEntity.StatsData.GetStat(Stats.Health)).AsSingle();
            subContainer.BindInstance(_enemyHealthUI).AsSingle();
        }
    }
}