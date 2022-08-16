using System;

namespace Core
{
    public class GameContext
    {
        public event Action OnReloadRequire;

        public void ReloadLevel()
        {
            OnReloadRequire?.Invoke();
        }
    }
}