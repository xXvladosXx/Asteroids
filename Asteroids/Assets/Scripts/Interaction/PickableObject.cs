using UnityEngine;

namespace Interaction
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class PickableObject : MonoBehaviour
    {
        protected Collider2D Collider;
        protected virtual void Awake()
        {
            Collider = GetComponent<Collider2D>();
        }
    }
}