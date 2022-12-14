using Controllers;
using Entities;
using StatsSystem;
using StatsSystem.Core;
using UI.Stats;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerHealthControllerInstaller : MonoInstaller
    {
        [SerializeField] private HealthUI _healthUI;

        public override void InstallBindings()
        {
            Container.Bind<HealthUI>().FromInstance(_healthUI).AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerHealthController>().AsSingle();
        }
    }
}