using Controllers;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField] private SettingsMenu _settingsMenu;

        public override void InstallBindings()
        {
            Container.Bind<SettingsMenu>().FromInstance(_settingsMenu);
            Container.BindInterfacesAndSelfTo<SettingsMenuController>().AsSingle();
        }
    }
}