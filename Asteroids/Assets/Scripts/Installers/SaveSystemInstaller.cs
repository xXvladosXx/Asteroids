using Saving;
using Zenject;

namespace Installers
{
    public class SaveSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SaveSystem>().AsSingle();
        }
    }
}