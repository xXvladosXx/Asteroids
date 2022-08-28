using AsteroidsZenject.EnemyShipZenject;
using EnemiesZenject.EnemyShipZenject;
using UnityEngine;
using Zenject;

namespace AsteroidZenject
{
    public class GameplaySignalsInstaller : Installer<GameplaySignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<EntityKilledSignal>();
        }
    }
}