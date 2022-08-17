using System;
using DG.Tweening;
using UI.Core;
using UnityEngine;

namespace UI.Fader
{
    public class FaderUI : PopupUIElement
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private bool _isFaded = false;

        public event Action OnFadeCompleted;
        public void Fader()
        {
            _isFaded = !_isFaded;

            if (_isFaded)
            {
                _canvasGroup.DOFade(1, 2).OnComplete(() =>
                {
                    OnFadeCompleted?.Invoke();
                });
            }
            else
            {
                _canvasGroup.DOFade(0, 2).OnComplete(() =>
                {
                    OnFadeCompleted?.Invoke();
                });;
            }
        }
    }
}