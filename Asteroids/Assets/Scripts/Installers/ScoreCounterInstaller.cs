using Controllers;
using Core;
using UI.Score;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ScoreCounterInstaller : MonoInstaller
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private ScoreCounterUI _scoreCounterUI;

        public override void InstallBindings()
        {
            Container.Bind<ScoreCounter>().FromInstance(_scoreCounter).AsSingle();
            Container.Bind<ScoreCounterUI>().FromInstance(_scoreCounterUI).AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreCounterController>().AsSingle();
        }
    }
}