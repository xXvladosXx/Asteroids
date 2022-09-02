using System;
using AudioSystem;
using Saving;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class AudioManagerController : IInitializable
    {
        private readonly SaveSystem _saveSystem;
        private readonly AudioManager _audioManager;

        public AudioManagerController(AudioManager audioManager, 
            SaveSystem saveSystem)
        {
            _audioManager = audioManager;
            _saveSystem = saveSystem;
        }

        public void Initialize()
        {
            _audioManager.ChangeMusicSound(_saveSystem.LoadSettings().MusicVolume);
            _audioManager.ChangeEffectsSound(_saveSystem.LoadSettings().EffectsVolume);
        }
    }
}