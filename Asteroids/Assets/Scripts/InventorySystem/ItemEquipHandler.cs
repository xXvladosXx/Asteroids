using System;
using InventorySystem.Core;
using UnityEngine;

namespace InventorySystem
{
    public class ItemEquipHandler : MonoBehaviour
    {
        [field: SerializeField] public int Capacity { get; private set; }

        private IContainer _itemContainer;
        private void Awake()
        {
            _itemContainer = new InventoryContainer(Capacity);
        }

        public void AddItem(IContainerItem containerItem)
        {
            _itemContainer.TryToAdd(this, containerItem);
        }
    }
}