using System;
using UnityEngine;

namespace Utilities.Input
{
    public class PlayerInput : MonoBehaviour
    {
        public InputActions InputActions { get; private set; }
        private InputActions.PlayerActionsActions PlayerActions { get; set; }

        public void Awake()
        {
            InputActions = new InputActions();
            
            PlayerActions = InputActions.PlayerActions;
        }

        private void OnEnable()
        {
            InputActions.Enable();
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }
    }
}