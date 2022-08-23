using UnityEngine;

namespace StateMachine.Core
{
    public abstract class StateMachine
    {
        private IState _currentState;

        public void ChangeState(IState newState)
        {
            _currentState?.Exit();

            _currentState = newState;

            _currentState.Enter();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void PhysicsUpdate()
        {
            _currentState?.FixedUpdate();
        }

        public void OnTriggerEnter(Collider collider)
        {
            _currentState?.OnTriggerEnter(collider);
        }

        public void OnTriggerExit(Collider collider)
        {
            _currentState?.OnTriggerExit(collider);
        }
    }
}