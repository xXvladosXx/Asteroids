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
            Container.DeclareSignal<AsteroidKilledSignal>();
            Container.DeclareSignal<EnemyShipKilledSignal>();
            
            Container.BindSignal<AsteroidKilledSignal>().ToMethod(() => Debug.Log("Killed asteroid"));
        }
    }
}