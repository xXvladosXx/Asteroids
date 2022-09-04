using System;

namespace InventorySystem.Core
{
    public interface IContainerItem
    {
        bool IsEquipped { get; set; }
        Type Type { get; }
        int MaxItemsInStack { get; }
        int Amount { get; set; }

        IContainerItem Clone();
    }
}