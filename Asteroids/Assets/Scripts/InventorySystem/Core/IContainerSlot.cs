using System;

namespace InventorySystem.Core
{
    public interface IContainerSlot
    {
        bool IsFull { get; }
        bool IsEmpty { get; }
        
        IContainerItem Item { get; }
        Type ItemType { get; }
        int Amount { get; }
        int Capacity { get; }

        void SetItem(IContainerItem item);
        void Clear();
    }
}