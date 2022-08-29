using AsteroidsZenject.AsteroidZenject;
using AsteroidsZenject.EnemyShipZenject;
using Spawners;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Settings/GameplaySettings")]
    public class GameplaySettingsInstaller : ScriptableObjectInstaller<GameplaySettingsInstaller>
    {
        [field: SerializeField] public AsteroidSpawner.Settings AsteroidSpawnerSettings { get; private set; }
        [field: SerializeField] public EnemyShipSpawner.Settings EnemyShipSpawnerInstaller { get; private set; }
        
        public override void InstallBindings()
        {
            Container.BindInstance(AsteroidSpawnerSettings).IfNotBound();
            Container.BindInstance(EnemyShipSpawnerInstaller).IfNotBound();
        }
    }
}