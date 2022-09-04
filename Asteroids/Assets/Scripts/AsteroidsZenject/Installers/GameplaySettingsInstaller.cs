using System.Collections.Generic;
using AsteroidsZenject;
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
        
        public override void InstallBindings()
        {
        }
    }
}