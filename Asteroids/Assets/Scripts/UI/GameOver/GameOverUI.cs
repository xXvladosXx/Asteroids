using System;
using UI.Core;
using UI.Fader;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameOver
{
    [RequireComponent(typeof(FaderUI))]
    public sealed class GameOverUI : PopupUIElement
    {
        [SerializeField] private Button _button;

        private FaderUI _fader;
        public override void Init(UIData uiData)
        {
            base.Init(uiData);
            
            _fader = GetComponent<FaderUI>();
            _fader.OnFadeCompleted += ActivateButton;
            
            _button.gameObject.SetActive(false);
        }

        private void ActivateButton()
        {
            _button.gameObject.SetActive(true);
            _button.onClick.AddListener(TryAgain);
        }

        public override void Show()
        {
            base.Show();
            
            _fader.Fader();
        }

        private void TryAgain()
        {
            UIData.GameContext.ReloadLevel();
        }

        private void OnDisable()
        {
            _fader.OnFadeCompleted -= ActivateButton;
            _button.onClick.RemoveAllListeners();
        }
    }
}