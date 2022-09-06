using Zenject;

namespace AsteroidsZenject
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