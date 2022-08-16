using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GameOver
{
    public sealed class GameOverUI : PopupUIElement
    {
        [SerializeField] private Button _button;

        public override void Init(UIData uiData)
        {
            base.Init(uiData);
            
            _button.onClick.AddListener(TryAgain);
        }

        private void TryAgain()
        {
            UIData.GameContext.ReloadLevel();
        }
    }
}