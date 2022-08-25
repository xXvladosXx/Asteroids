using Controllers;
using EnemyShipZenject;
using Entities;
using StatsSystem;
using StatsSystem.Core;
using UI.Stats;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemyShipInstaller : MonoInstaller
    {
        [SerializeField] private EnemyShip _enemyEntity;
        [SerializeField] private EnemyHealthUI _enemyHealthUI;

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