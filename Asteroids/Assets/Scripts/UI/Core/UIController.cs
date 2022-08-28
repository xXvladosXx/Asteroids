using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

namespace UI.Core
{
    public abstract class UIController : MonoBehaviour
    {
        private UIData _uiData;
        private List<StaticUIElement> _staticUIElements = new List<StaticUIElement>();
        private List<PopupUIElement> _popupUIElements  = new List<PopupUIElement>();

        private PopupUIElement _currentPopupUIElement;
        
        public event Action OnPopupUIOpened;
        public event Action OnPopupUIHide;
        
        public virtual void Init(UIData uiData)
        {
            _staticUIElements = GetComponentsInChildren<StaticUIElement>().ToList();
            _popupUIElements = GetComponentsInChildren<PopupUIElement>().ToList();
            
            _uiData = uiData;
            
            foreach (var popupUIElement in _popupUIElements)
            {
                popupUIElement.Init(_uiData);
                popupUIElement.Hide();
                popupUIElement.OnElementShow += OnPopupShow;
                popupUIElement.OnElementHide += OnPopupHide;
            }

            foreach (var staticUIElement in _staticUIElements)
            {
                staticUIElement.Init(_uiData);
            }
        }

        private void OnPopupHide()
        {
            OnPopupUIHide?.Invoke();
        }

        private void OnPopupShow(IUIElement obj)
        {
            OnPopupUIOpened?.Invoke();
        }

        public void SwitchUIElement<T>() where T : PopupUIElement
        {
            if (_currentPopupUIElement != null)
            {
                if (_currentPopupUIElement.GetType() == typeof(T))
                {
                    HideUIElement();
                    return;
                }

                HideUIElement();
            }

            ShowUIElement<T>();
        }

        private void ShowUIElement<T>() where T : PopupUIElement
        {
            var uiElement = _popupUIElements.FirstOrDefault(e => e.GetType() == typeof(T));
            if (uiElement != null)
            {
                uiElement.Show();
                _currentPopupUIElement = uiElement;
                _currentPopupUIElement.OnElementHide += HideUIElement;
            }
        }

        public void HideUIElement()
        {
            _currentPopupUIElement.OnElementHide -= HideUIElement;
            _currentPopupUIElement.Hide();
            _currentPopupUIElement = null;
        }

        protected virtual void OnDisable()
        {
            foreach (var popupUIElement in _popupUIElements)
            {
                popupUIElement.OnElementShow -= OnPopupShow;
                popupUIElement.OnElementHide -= OnPopupHide;
            }
        }
    }
}