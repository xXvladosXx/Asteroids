using System;
using UnityEngine;

namespace Entities.Core
{
    public abstract class Entity : MonoBehaviour
    {
        protected virtual void Awake()
        {
            
        }

        public abstract void Die();
    }
}