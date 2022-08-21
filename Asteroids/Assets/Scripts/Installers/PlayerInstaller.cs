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
            Container.Bind<Heath>().FromSubContainerResolve().ByMethod(InstallPlayerHealth).AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerEntity>().FromInstance(_playerEntity).AsSingle();
            Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle();
        }

        private void InstallPlayerHealth(DiContainer diContainer)
        {
            diContainer.Bind<Heath>().AsSingle();
            diContainer.BindInstance(_playerEntity.StatsData.GetStat(Stats.Health)).AsSingle();
        }
    }
}