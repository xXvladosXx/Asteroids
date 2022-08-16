using System;
using Data.Score;
using Entities;
using Spawners;
using UI.Core;
using UI.GameOver;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public sealed class Game : MonoBehaviour
    {
        [field: SerializeField] public ScoreCounter ScoreCounter { get; private set; }

        [SerializeField] private PlayerEntity _player;
        [SerializeField] private UIController _uiController;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;

        private GameContext _gameContext;
        private void Awake()
        {
            _gameContext = new GameContext();
            
            _uiController.Init(new UIData
            {
                Player = _player,
                ScoreCounter = ScoreCounter,
                GameContext = _gameContext
            });
        }

        
        private void OnEnable()
        {
            _asteroidSpawner.OnScoreAdded += ScoreCounter.AddScore;
            _player.OnDied += OnGameEnd;
            _gameContext.OnReloadRequire += ReloadLevel;
        }

        private void OnGameEnd()
        {
            _uiController.SwitchUIElement<GameOverUI>();    
        }
        
        private void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void OnDisable()
        {
            _asteroidSpawner.OnScoreAdded -= ScoreCounter.AddScore;
            _player.OnDied -= OnGameEnd;
            _gameContext.OnReloadRequire -= ReloadLevel;
        }
    }
}