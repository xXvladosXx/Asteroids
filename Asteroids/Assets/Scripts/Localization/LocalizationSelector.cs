using System;
using System.Collections;
using Saving;
using UnityEngine.Localization.Settings;
using Utilities;
using Zenject;

namespace Localization
{
    public class LocalizationSelector : IInitializable, IDisposable
    {
        private readonly SaveSystem _saveSystem;

        public LocalizationSelector(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }
        
        public void Initialize()
        {
            int id = _saveSystem.LoadSettings().Language;
            ChangeLocale(id);
        }

        public void ChangeLocale(int localId)
        {
            Coroutines.StartRoutine(SetLocale(localId));
        }

        private IEnumerator SetLocale(int localId)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localId];
            _saveSystem.SetLanguage(localId);   
        }

        public void Dispose()
        {
        }
    }
}