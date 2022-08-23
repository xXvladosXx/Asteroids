using Entities.Core;
using StateMachine.Core;
using UnityEngine;

namespace StateMachine.Enemy.BaseStates
{
    public class AttackState: IState
    {
        private readonly IEnemy _enemy;
        
        public AttackState(IEnemy enemy)
        {
            _enemy = enemy;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void OnTriggerEnter(Collider collider)
        {
            
        }

        public void OnTriggerExit(Collider collider)
        {
            
        }
    }
}