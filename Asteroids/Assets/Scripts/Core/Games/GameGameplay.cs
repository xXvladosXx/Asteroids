using System;
using AsteroidsZenject.AsteroidZenject;
using AudioSystem;
using Camera;
using Combat.Core;
using Data.Camera;
using Entities;
using Saving;
using Spawners;
using UI.Core;
using UI.GameOver;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Games
{
    public sealed class GameGameplay : MonoBehaviour
    {
        [field: SerializeField] public CameraShakerData CameraShakerData { get; private set; }
        
        private GameContext _gameContext;
        private SaveSystem _saveSystem;
        
        private PlayerEntity _player;
        private UIController _uiController;
        private ScoreCounter _scoreCounter;
        private AudioManager _audioManager;

        [Inject]
        private void Construct(PlayerEntity playerEntity, 
            UIController uiController, AsteroidSpawner asteroidSpawner, 
            ScoreCounter scoreCounter, AudioManager audioManager)
        {
            _player = playerEntity;
            _uiController = uiController;
            _scoreCounter = scoreCounter;
            _audioManager = audioManager;
        }
        private void Awake()
        {
            _gameContext = new GameContext();
            _saveSystem = new SaveSystem();

            _uiController.Init(new UIData
            {
                Player = _player,
                ScoreCounter = _scoreCounter,
                GameContext = _gameContext,
                SaveSystem = _saveSystem
            });
        }

        private void Start()
        {
            var settingsData = _saveSystem.LoadSettings();
            
            _audioManager.ChangeEffectsSound(settingsData.EffectsVolume);
            _audioManager.ChangeMusicSound(settingsData.MusicVolume);
        }

        private void OnEnable()
        {
            _player.Heath.OnDied += StartCameraShaking;
            _gameContext.OnReloadRequire += ReloadLevel;
        }

        private void StartCameraShaking(IAttackApplier attackApplier)
        {
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void OnDisable()
        {
            _player.Heath.OnDied -= StartCameraShaking;
            _gameContext.OnReloadRequire -= ReloadLevel;
        }
    }
}