using Combat.Projectiles;
using UnityEngine;
using Zenject;

namespace AsteroidsZenject.ProjectileZenject
{
    public class ProjectileInstaller : MonoInstaller
    {
        [SerializeField] public OrdinaryProjectile OrdinaryProjectile;
        public override void InstallBindings()
        {
            Container.BindFactory<OrdinaryProjectile, OrdinaryProjectile.Factory>()
                .FromPoolableMemoryPool<OrdinaryProjectile, OrdinaryProjectilePool>(poolBinder => poolBinder
                    .WithInitialSize(100)
                    .FromSubContainerResolve()
                    .ByNewPrefabMethod(OrdinaryProjectile, InstallOrdinaryProjectile)
                    .UnderTransformGroup("Bullets"));
        }

        private void InstallOrdinaryProjectile(DiContainer subContainer)
        {
            subContainer.Bind<OrdinaryProjectile>().FromComponentOnRoot().AsSingle();
        }
        
        public class OrdinaryProjectilePool : MonoPoolableMemoryPool<IMemoryPool, OrdinaryProjectile>
        {
        }
    }
}