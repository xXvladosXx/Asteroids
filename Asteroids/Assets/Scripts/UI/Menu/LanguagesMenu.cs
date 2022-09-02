using System;
using UI.Core;
using UI.Menu.Core;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Extensions;

namespace UI.Menu
{
    public class LanguagesMenu : Core.Menu
    {
        [SerializeField] private Button _english;
        [SerializeField] private Button _russian;
        [SerializeField] private Button _polish;
        [SerializeField] private Button _ukrainian;
        
        [SerializeField] private Button _backButton;

        public event Action<int> OnLanguageChanged; 
        public override void Init()
        {
        }
        
        private void OnEnable()
        {
            _backButton.onClick.AddListener(SwitchToLast);
            
            _english.onClick.AddListener(() =>OnLanguageChanged?.Invoke(0));
            _polish.onClick.AddListener(() =>OnLanguageChanged?.Invoke(1));
            _russian.onClick.AddListener(() =>OnLanguageChanged?.Invoke(2));
            _ukrainian.onClick.AddListener(() =>OnLanguageChanged?.Invoke(3));
        }

        private void SwitchToLast()
        {
            this.CallWithDelay(MainMenuSwitcher.ShowLast, 0.05f); 
        }
        
        private void OnDisable()
        {
            _backButton.onClick.AddListener(SwitchToLast);
            
            _english.onClick.RemoveAllListeners();
            _polish.onClick.RemoveAllListeners();
            _russian.onClick.RemoveAllListeners();
            _ukrainian.onClick.RemoveAllListeners();
        }
    }
}