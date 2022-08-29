using UI.Enemy;
using UnityEngine;
using Zenject;

namespace AsteroidsZenject
{
    public class EnemiesUIInstaller : MonoInstaller
    {
        [SerializeField] private EnemiesUI _enemiesUI;

        public override void InstallBindings()
        {
            Container.Bind<EnemiesUI>().FromInstance(_enemiesUI).AsSingle();
        }
    }
}