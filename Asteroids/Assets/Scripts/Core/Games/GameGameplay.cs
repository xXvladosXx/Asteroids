using System;
using AudioSystem;
using Camera;
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
        private AsteroidSpawner _asteroidSpawner;
        private ScoreCounter _scoreCounter;

        [Inject]
        private void Construct(PlayerEntity playerEntity, 
            UIController uiController, AsteroidSpawner asteroidSpawner, 
            ScoreCounter scoreCounter)
        {
            _player = playerEntity;
            _uiController = uiController;
            _asteroidSpawner = asteroidSpawner;
            _scoreCounter = scoreCounter;
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
            
            AudioManager.Instance.ChangeEffectsSound(settingsData.EffectsVolume);
            AudioManager.Instance.ChangeMusicSound(settingsData.MusicVolume);
        }

        private void OnEnable()
        {
            _player.OnDied += StartCameraShaking;
            _gameContext.OnReloadRequire += ReloadLevel;
        }

        private void StartCameraShaking()
        {
            CameraShaker.Instance.StartShaking(CameraShakerData.Time, CameraShakerData.Magnitude);
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void OnDisable()
        {
            _player.OnDied -= StartCameraShaking;
            _gameContext.OnReloadRequire -= ReloadLevel;
        }
    }
}