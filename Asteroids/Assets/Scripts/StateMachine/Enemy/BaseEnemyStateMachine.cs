using Entities.Core;

namespace StateMachine.Enemy
{
    public class BaseEnemyStateMachine : Core.StateMachine
    {
        private readonly IEnemy _enemy;

        public BaseEnemyStateMachine(IEnemy enemy)
        {
            _enemy = enemy;
        }
    }
}