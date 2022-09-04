using System;
using UnityEngine;

namespace UI.Core
{
    public abstract class UIElement : MonoBehaviour, IUIElement
    {
        
        public event Action OnElementHide;
        public event Action<IUIElement> OnElementShow;

        public virtual void Init() { }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnElementHide?.Invoke();
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            OnElementShow?.Invoke(this);
        }
    }
}