using Entities;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity _playerEntity;
        public override void InstallBindings()
        {
            Container.Bind<PlayerEntity>().FromInstance(_playerEntity).AsSingle().NonLazy();
        }
    }
}