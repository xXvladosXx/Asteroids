using System;
using AsteroidsZenject;
using UI.Core;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Extensions;

namespace UI.Menu
{
    public class DifficultyMenu : Core.Menu
    {
        [SerializeField] private Button _easyButton;
        [SerializeField] private Button _mediumButton;
        [SerializeField] private Button _hardButton;
        [SerializeField] private Button _backButton;

        public event Action<int> OnStartSceneLoadRequested; 

        public override void Init()
        {
            _easyButton.onClick.AddListener(() => StartGame(0));
            _mediumButton.onClick.AddListener(() => StartGame(1));
            _hardButton.onClick.AddListener(() => StartGame(2));
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(SwitchToLast);
        }

        private void SwitchToLast()
        {
            this.CallWithDelay(MainMenuSwitcher.ShowLast, 0.05f); 
        }
        private void StartGame(int levelIndex)
        {
            OnStartSceneLoadRequested?.Invoke(levelIndex);
        }
        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(SwitchToLast);
        }
    }
}