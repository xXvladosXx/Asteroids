using Saving;
using UI.Core;
using UI.GameOver;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core.Games
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private UIController _uiController;

        private GameContext _gameContext;

        private void Awake()
        {
            
        }
    }
}