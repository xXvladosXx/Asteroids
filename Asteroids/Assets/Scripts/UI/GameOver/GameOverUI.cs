using System;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private FaderUI _fader;

        public event Action OnTryAgainRequest;
        public void Init()
        {
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

        public void ChangeScore(string result)
        {
        }

        private void TryAgain()
        {
            OnTryAgainRequest?.Invoke();
        }

        private void OnDisable()
        {
            _fader.OnFadeCompleted -= ActivateButton;
            _button.onClick.RemoveAllListeners();
        }
    }
}