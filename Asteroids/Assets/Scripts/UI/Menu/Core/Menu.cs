using UI.Core;
using UnityEngine;

namespace UI.Menu.Core
{
    public abstract class Menu : MonoBehaviour
    {
        protected string SaveFile = " ";
        
        public virtual void Init() { }
        public virtual void HideMenu() => gameObject.SetActive(false);
        public virtual void ShowMenu() => gameObject.SetActive(true);
    }
}