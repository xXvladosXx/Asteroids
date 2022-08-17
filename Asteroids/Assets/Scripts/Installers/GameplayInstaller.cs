using Entities;
using Spawners;
using UI.Core;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private UIController _uiController;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindUIController();
            BindAsteroidSpawner();
        }

        private void BindAsteroidSpawner()
        {
            Container.Bind<AsteroidSpawner>().FromInstance(_asteroidSpawner).AsSingle();
        }

        private void BindUIController()
        {
            Container.Bind<UIController>().FromInstance(_uiController).AsSingle();
        }

        private void BindPlayer()
        {
            Container.Bind<PlayerEntity>().FromInstance(_playerEntity).AsSingle().NonLazy();
        }
    }
}