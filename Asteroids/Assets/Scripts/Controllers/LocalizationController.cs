using System;
using Localization;
using UI.Menu;
using Zenject;

namespace Controllers
{
    public class LocalizationController : IInitializable, IDisposable
    {
        private readonly LocalizationSelector _localizationSelector;
        private readonly LanguagesMenu _languageMenu;

        public LocalizationController(LanguagesMenu languagesMenu, LocalizationSelector localizationSelector)
        {
            _languageMenu = languagesMenu;
            _localizationSelector = localizationSelector;
        }

        public void Initialize()
        {
            _languageMenu.OnLanguageChanged += ChangeLanguage;
        }

        private void ChangeLanguage(int id)
        {
            _localizationSelector.ChangeLocale(id);
        }

        public void Dispose()
        {
            _languageMenu.OnLanguageChanged -= ChangeLanguage;
        }
    }
}