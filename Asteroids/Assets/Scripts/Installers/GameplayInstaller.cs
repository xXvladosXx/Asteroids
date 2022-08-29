using AudioSystem;
using Camera;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private CameraShaker _cameraShaker;
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().FromInstance(_audioManager).AsSingle();
            Container.Bind<CameraShaker>().FromInstance(_cameraShaker).AsSingle();
        }
    }
}