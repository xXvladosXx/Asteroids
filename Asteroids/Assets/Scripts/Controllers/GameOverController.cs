using System;
using AsteroidsZenject.PlayerZenject;
using Core;
using LevelSystem;
using Saving;
using UI.GameOver;
using Zenject;

namespace Controllers
{
    public class GameOverController : IInitializable, IDisposable
    {
        private readonly PlayerDeathHandler _playerDeathHandler;
        private readonly GameOverUI _gameOverUI;
        private readonly SaveSystem _saveSystem;
        private readonly LevelLoader _levelLoader;

        public GameOverController(PlayerDeathHandler playerDeathHandler,
            GameOverUI gameOverUI, SaveSystem saveSystem,
            LevelLoader levelLoader)
        {
            _playerDeathHandler = playerDeathHandler;
            _gameOverUI = gameOverUI;
            _saveSystem = saveSystem;
            _levelLoader = levelLoader;
        }

        public void Initialize()
        {
            _gameOverUI.Init();

            _gameOverUI.OnTryAgainRequest += _levelLoader.ReloadLevel;  
            _playerDeathHandler.OnPlayerDied += ShowGameOverUI;
        }

        private void ShowGameOverUI()
        {
            _gameOverUI.ChangeScore(_saveSystem.GetBestScore());
            _gameOverUI.Show();
        }

        public void Dispose()
        {
            _gameOverUI.OnTryAgainRequest -= _levelLoader.ReloadLevel;  
            _playerDeathHandler.OnPlayerDied -= ShowGameOverUI;
        }
    }
}