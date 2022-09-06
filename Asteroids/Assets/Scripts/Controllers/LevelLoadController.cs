using System;
using AsteroidsZenject;
using LevelSystem;
using UI.Menu;
using Zenject;

namespace Controllers
{
    public class LevelLoadController : IInitializable, IDisposable
    {
        private readonly LevelLoader _levelLoader;
        private readonly DifficultyMenu _difficultyMenu;
        private readonly DifficultyManager _difficultyManager;

        private const int START_LEVEL_INDEX = 1;
        public LevelLoadController(LevelLoader levelLoader,
            DifficultyMenu difficultyMenu,
            DifficultyManager difficultyManager)
        {
            _levelLoader = levelLoader;
            _difficultyMenu = difficultyMenu;
            _difficultyManager = difficultyManager;
        }

        public void Initialize()
        {
            _difficultyMenu.OnStartSceneLoadRequested += LoadStartLevel;
        }

        private void LoadStartLevel(int difficultyLevel)
        {
            _difficultyManager.ChangeDifficulty(difficultyLevel);

            _levelLoader.LoadScene(START_LEVEL_INDEX);
        }

        public void Dispose()
        {
            _difficultyMenu.OnStartSceneLoadRequested -= LoadStartLevel;
        }
    }
}