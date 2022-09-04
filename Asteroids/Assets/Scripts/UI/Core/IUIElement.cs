using System;

namespace UI.Core
{
    public interface IUIElement
    {
        public event Action OnElementHide;
        public event Action<IUIElement> OnElementShow;

        void Init();
        void Hide();
        void Show();
    }
}