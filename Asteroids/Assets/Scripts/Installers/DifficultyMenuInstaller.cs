using AsteroidsZenject;
using Controllers;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DifficultyMenuInstaller : MonoInstaller
    {
        [SerializeField] private DifficultyMenu _difficultyMenu;
        public override void InstallBindings()
        {
            Container.Bind<DifficultyMenu>().FromInstance(_difficultyMenu).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelLoadController>().AsSingle();
        }
    }
}