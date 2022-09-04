using System;
using InventorySystem.Core;

namespace InventorySystem
{
    public class InventorySlot : IContainerSlot
    {
        public bool IsFull => Amount == Capacity;
        public bool IsEmpty => Item == null;
        public IContainerItem Item { get; private set; }
        public Type ItemType => Item.Type;
        public int Amount => IsEmpty ? 0 : Item.Amount;
        public int Capacity { get; private set; }
        
        public void SetItem(IContainerItem item)
        {
            if(!IsEmpty)
                return;

            Item = item;
            Capacity = Item.MaxItemsInStack;
        }

        public void Clear()
        {
            if(IsEmpty)
                return;

            Item.Amount = 0;
            Item = null;
        }
    }
}