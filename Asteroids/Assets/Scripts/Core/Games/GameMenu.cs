using Saving;
using UI.Core;
using UI.GameOver;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Games
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;

        private GameContext _gameContext;
        private SaveSystem _saveSystem;

        private void Awake()
        {
            _gameContext = new GameContext();
            _saveSystem = new SaveSystem();
            
            _uiController.Init(new UIData
            {
                GameContext = _gameContext,
                SaveSystem = _saveSystem
            });
        }
    }
}