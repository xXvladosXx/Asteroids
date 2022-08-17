using System;
using Entities;
using UI.Core;
using UI.GameOver;

namespace UI.UIControllers
{
    public class GameplayUIController : UIController
    {
        private PlayerEntity _player;
        public override void Init(UIData uiData)
        {
            base.Init(uiData);

            _player = uiData.Player;
            
            _player.OnDied += SwitchUIElement<GameOverUI>;
        }

        protected override void OnDisable()
        {
            _player.OnDied -= SwitchUIElement<GameOverUI>;
        }
    }
}