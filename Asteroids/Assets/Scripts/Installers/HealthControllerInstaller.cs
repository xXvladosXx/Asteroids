using Controllers;
using Entities;
using StatsSystem;
using StatsSystem.Core;
using UI.Stats;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class HealthControllerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private HealthUI _healthUI;

        public override void InstallBindings()
        {
            Container.Bind<Heath>().FromSubContainerResolve().ByMethod(InstallPlayerHealth).AsSingle();
            Container.Bind<HealthUI>().FromInstance(_healthUI).AsSingle();
            Container.BindInterfacesAndSelfTo<HealthController>().AsSingle();
        }

        private void InstallPlayerHealth(DiContainer obj)
        {
            obj.Bind<Heath>().AsSingle();
            obj.BindInstance(_playerEntity.StatsData.GetStat(Stats.Health)).AsSingle();
        }
    }
}