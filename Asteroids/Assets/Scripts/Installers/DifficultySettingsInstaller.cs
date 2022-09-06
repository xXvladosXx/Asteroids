

using System.ComponentModel;
using Data.Difficulties;
using UnityEngine;
using Zenject;

namespace AsteroidsZenject.Installers
{
    [CreateAssetMenu(menuName = "Settings/DifficultySettings")]
    public class DifficultySettingsInstaller : ScriptableObjectInstaller<DifficultySettingsInstaller>
    {
        [field: SerializeField] public DifficultiesData DifficultiesData { get; private set; }
        public override void InstallBindings()
        {
            Container.BindInstance(DifficultiesData).IfNotBound();
        }
    }
}