using UnityEngine;

namespace StateMachine.Enemy.States
{
    public interface ITargetFinder
    {
        Collider2D[] PossibleTargets(LayerMask layerMask, Vector2 centre, Vector2 size, float angle);
        void FindTarget(Collider2D[] colliders);
    }
}