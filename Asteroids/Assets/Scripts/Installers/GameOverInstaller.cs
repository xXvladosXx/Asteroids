using AsteroidsZenject.PlayerZenject;
using Controllers;
using UI.GameOver;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameOverInstaller : MonoInstaller
    {
        [SerializeField] private GameOverUI _gameOverUI;
        
        public override void InstallBindings()
        {
            Container.Bind<GameOverUI>().FromInstance(_gameOverUI);
            Container.BindInterfacesAndSelfTo<PlayerDeathHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverController>().AsSingle();
        }
    }
}