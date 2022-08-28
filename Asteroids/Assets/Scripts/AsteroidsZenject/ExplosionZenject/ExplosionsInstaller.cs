using UnityEngine;
using Zenject;

namespace AsteroidsZenject.ExplosionZenject
{
    public class ExplosionsInstaller : MonoInstaller
    {
        [SerializeField] private EnemyExplosion enemyExplosion;
        
        public override void InstallBindings()
        {
            Container.BindFactory<EnemyExplosion, EnemyExplosion.Factory>()
                .FromPoolableMemoryPool<EnemyExplosion, ExplosionPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(enemyExplosion)
                    .UnderTransformGroup("Explosions"));
        }
        
        class ExplosionPool : MonoPoolableMemoryPool<IMemoryPool, EnemyExplosion>
        {
        }
    }
}