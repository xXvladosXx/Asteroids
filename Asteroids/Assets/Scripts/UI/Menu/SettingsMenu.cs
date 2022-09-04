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
        [SerializeField] private Button _languages;

        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectsSlider;

        private int _screenInt;

        public event Action<SaveData> OnSaveDataRequest;
        public event Action<float> OnEffectsVolumeChanged; 
        public event Action<float> OnMusicVolumeChanged; 

        public void SetEffectsVolume(float volume)
        {
            OnEffectsVolumeChanged?.Invoke(volume);
            //_audioManager.ChangeEffectsSound(volume);
        }

        public void SetMusicVolume(float volume)
        {
            OnMusicVolumeChanged?.Invoke(volume);
            //_audioManager.ChangeMusicSound(volume);
        }

        public void SaveSettings()
        {
            SaveData settingsSaveData = new SaveData
            {
                MusicVolume = _musicSlider.value,
                EffectsVolume = _effectsSlider.value,
            };

            OnSaveDataRequest?.Invoke(settingsSaveData);
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(SwitchToLast);
            _languages.onClick.AddListener(SwitchToLanguages);
        }

        private void SwitchToLanguages()
        {
            this.CallWithDelay(() => MainMenuSwitcher.Show<LanguagesMenu>(), 0.05f); 
        }

        private void SwitchToLast()
        {
            SaveSettings();
            this.CallWithDelay(MainMenuSwitcher.ShowLast, 0.05f); 
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(SwitchToLast);
            _languages.onClick.RemoveListener(SwitchToLanguages);
        }

        public void LoadLastSettings(SaveData saveData)
        {
            var settings = saveData;

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