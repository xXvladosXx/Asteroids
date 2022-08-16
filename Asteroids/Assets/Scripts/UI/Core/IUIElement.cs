using System;

namespace UI.Core
{
    public interface IUIElement
    {
        public event Action OnElementHide;
        public event Action<IUIElement> OnElementShow;

        void Init(UIData player);
        void Hide();
        void Show();
    }
}