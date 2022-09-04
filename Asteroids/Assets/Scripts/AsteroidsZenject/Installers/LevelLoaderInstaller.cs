using Controllers;
using LevelSystem;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace AsteroidsZenject.Installers
{
    public class LevelLoaderInstaller : MonoInstaller
    {
        [SerializeField] private LevelLoader _levelLoader;
        public override void InstallBindings()
        {
            Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle();
            Container.Bind<DifficultyManager>().AsSingle();
        }
    }
}