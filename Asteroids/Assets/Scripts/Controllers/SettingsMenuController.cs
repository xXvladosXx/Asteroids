using System;
using AudioSystem;
using Saving;
using UI.Menu;
using Zenject;

namespace Controllers
{
    public class SettingsMenuController : IInitializable, IDisposable
    {
        private readonly SettingsMenu _settingsMenu;
        private readonly SaveSystem _saveSystem;
        private readonly AudioManager _audioManager;

        public SettingsMenuController(SaveSystem saveSystem, 
            SettingsMenu settingsMenu, AudioManager audioManager)
        {
            _saveSystem = saveSystem;
            _settingsMenu = settingsMenu;
            _audioManager = audioManager;
        }

        public void Initialize()
        {
            _settingsMenu.OnSaveDataRequest += _saveSystem.SaveSettings;
            _settingsMenu.OnEffectsVolumeChanged += _audioManager.ChangeEffectsSound;
            _settingsMenu.OnMusicVolumeChanged += _audioManager.ChangeMusicSound;
            
            _settingsMenu.LoadLastSettings(_saveSystem.LoadSettings());
        }

        public void Dispose()
        {
            _settingsMenu.OnSaveDataRequest -= _saveSystem.SaveSettings;
            _settingsMenu.OnEffectsVolumeChanged -= _audioManager.ChangeEffectsSound;
            _settingsMenu.OnMusicVolumeChanged -= _audioManager.ChangeMusicSound;
        }
    }
}