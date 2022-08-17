using UnityEngine;
using Utilities.Input;
using Zenject;

namespace Installers
{
    public class PlayerInputInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInput _playerInput;

        public override void InstallBindings()
        {
            Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle();
        }
    }
}