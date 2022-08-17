using System;
using AudioSystem;
using Camera;
using Data.Camera;
using Entities;
using Saving;
using Spawners;
using UI.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Games
{
    public sealed class GameGameplay : MonoBehaviour
    {
        [field: SerializeField] public ScoreCounter ScoreCounter { get; private set; }
        [field: SerializeField] public CameraShakerData CameraShakerData { get; private set; }
        
        [SerializeField] private PlayerEntity _player;
        [SerializeField] private UIController _uiController;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;

        private GameContext _gameContext;
        private SaveSystem _saveSystem;

        private void Awake()
        {
            _gameContext = new GameContext();
            _saveSystem = new SaveSystem();

            _uiController.Init(new UIData
            {
                Player = _player,
                ScoreCounter = ScoreCounter,
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
            _asteroidSpawner.OnScoreAdded += ScoreCounter.AddScore;
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
            _asteroidSpawner.OnScoreAdded -= ScoreCounter.AddScore;
            _player.OnDied -= StartCameraShaking;
            _gameContext.OnReloadRequire -= ReloadLevel;
        }
    }
}