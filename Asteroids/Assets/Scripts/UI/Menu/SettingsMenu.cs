using System;
using System.Collections.Generic;
using AudioSystem;
using Saving;
using TMPro;
using UI.Core;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Utilities.Extensions;
using Zenject;

namespace UI.Menu
{
    public class SettingsMenu : Core.Menu
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Toggle _fullScreen;

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;

        private int _screenInt;

        private SaveSystem _saveSystem;
        private AudioManager _audioManager;

        [Inject]
        public void Construct(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }
        
        public override void Init(UIData uiData)
        {
            base.Init(uiData);

            _saveSystem = uiData.SaveSystem;

            LoadLastSettings();
        }

        public void SetEffectsVolume(float volume)
        {
            _audioManager.ChangeEffectsSound(volume);
        }

        public void SetMusicVolume(float volume)
        {
            _audioManager.ChangeMusicSound(volume);
        }

        public void SaveSettings()
        {
            SaveData settingsSaveData = new SaveData
            {
                MusicVolume = _musicSlider.value,
                EffectsVolume = _effectsSlider.value,
            };

            _saveSystem.SaveSettings(settingsSaveData);
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(TryToSwitchMenu);
        }

        private void TryToSwitchMenu()
        {
            SaveSettings();
            this.CallWithDelay(MainMenuSwitcher.ShowLast, 0.05f); 
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
        }

        private void LoadLastSettings()
        {
            var settings = _saveSystem.LoadSettings();

            FindEffectsVolume(settings);
            FindMusicVolume(settings);
        }

        private void FindEffectsVolume(SaveData settings)
        {
            _effectsSlider.value = settings.EffectsVolume;
            SetEffectsVolume(settings.EffectsVolume);
        }

        private void FindMusicVolume(SaveData settings)
        {
            _musicSlider.value = settings.MusicVolume;
            SetMusicVolume(settings.MusicVolume);
        }

        private void FindFullscreenOption(SaveData settings)
        {
           // _fullScreen.isOn = settings.Fullscreen;
           // SetFullscreen(_fullScreen.isOn);
        }
    }
}