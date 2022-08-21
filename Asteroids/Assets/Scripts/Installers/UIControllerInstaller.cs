using UI.Core;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIControllerInstaller : MonoInstaller
    {
        [SerializeField] private UIController _uiController;

        public override void InstallBindings()
        {
            Container.Bind<UIController>().FromInstance(_uiController).AsSingle();
        }
    }
}