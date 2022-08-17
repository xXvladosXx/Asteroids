using Combat;
using Interaction;
using UnityEngine;

namespace Entities.Core
{
    [RequireComponent(typeof(AttackMaker))]
    public abstract class ShipEntity : Entity
    {
        [field: SerializeField] public ObjectPicker ObjectPicker { get; private set; }

        protected AttackMaker AttackMaker;

        protected override void Awake()
        {
            AttackMaker = GetComponent<AttackMaker>();
            AttackMaker.Init(ObjectPicker);
            
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }
    }
}