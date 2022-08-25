using System;
using Entities;
using UI.Core;
using UI.GameOver;
using Zenject;

namespace UI.UIControllers
{
    public class GameplayUIController : UIController
    {
        private PlayerEntity _player;

        [Inject]
        public void Construct(PlayerEntity playerEntity)
        {
            _player = playerEntity;
        }
        
        public override void Init(UIData uiData)
        {
            base.Init(uiData);

            _player.OnDied += SwitchUIElement<GameOverUI>;
        }

        protected override void OnDisable()
        {
            _player.OnDied -= SwitchUIElement<GameOverUI>;
        }
    }
}