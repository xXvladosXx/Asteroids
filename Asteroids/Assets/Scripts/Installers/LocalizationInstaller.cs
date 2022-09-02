using Controllers;
using Localization;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LocalizationInstaller : MonoInstaller
    {
        [SerializeField] private LanguagesMenu _languagesMenu;
        public override void InstallBindings()
        {
            Container.Bind<LanguagesMenu>().FromInstance(_languagesMenu).AsSingle();
            Container.BindInterfacesAndSelfTo<LocalizationSelector>().AsSingle();
            Container.BindInterfacesAndSelfTo<LocalizationController>().AsSingle();
        }
    }
}