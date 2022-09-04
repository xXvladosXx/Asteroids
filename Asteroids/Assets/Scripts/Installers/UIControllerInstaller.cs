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
            Container.BindInterfacesAndSelfTo<UIController>().FromInstance(_uiController).AsSingle();
        }
    }
}