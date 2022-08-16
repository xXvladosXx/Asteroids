using System;

namespace Core
{
    public sealed class GameContext
    {
        public event Action OnReloadRequire;

        public void ReloadLevel()
        {
            OnReloadRequire?.Invoke();
        }
    }
}