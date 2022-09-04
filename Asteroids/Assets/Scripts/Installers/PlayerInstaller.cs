using AsteroidsZenject;
using Entities;
using StatsSystem;
using StatsSystem.Core;
using UnityEngine;
using Utilities.Input;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private PlayerInput _playerInput;

        public override void InstallBindings()
        {
            var difficultyData = Container.Resolve<DifficultyManager>().DifficultyData;
            
            Container.BindInstance(difficultyData.PlayerSettingsSo);
            Container.Bind<Heath>().FromSubContainerResolve().ByMethod(InstallPlayerHealth).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerEntity>().FromInstance(_playerEntity).AsSingle();
            Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle();
        }

        private void InstallPlayerHealth(DiContainer subContainer)
        {
            subContainer.Bind<Heath>().AsSingle();
            subContainer.BindInstance(_playerEntity.StatsData.GetStat(Stats.Health)).AsSingle();
        }
    }
}