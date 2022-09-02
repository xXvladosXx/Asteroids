using LevelSystem;
using UI.Core;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utilities.Extensions;

namespace UI.Menu
{
    public class MainMenu : Core.Menu
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingButton;
        [SerializeField] private Button _quitButton;

        public override void Init()
        {
            base.Init();
         
            _startButton.onClick.AddListener(() => this.CallWithDelay(SwitchToDifficulty, .05f));
            _settingButton.onClick.AddListener(() => this.CallWithDelay(SwitchToSettings, .05f));
            _quitButton.onClick.AddListener( () =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
            });
        }

        private void SwitchToDifficulty()
        {
            MainMenuSwitcher.Show<DifficultyMenu>();
        }

        private void SwitchToSettings()
        {
            MainMenuSwitcher.Show<SettingsMenu>();
        }

        
    }

}
