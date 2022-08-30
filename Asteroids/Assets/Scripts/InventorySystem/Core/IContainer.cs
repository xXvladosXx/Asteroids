using System;

namespace InventorySystem.Core
{
    public interface IContainer
    {
        int Capacity { get; set; }
        bool IsFull { get; }

        IContainerItem GetItem(Type itemType);
        IContainerItem[] GetAllItems();
        IContainerItem[] GetAllItems(Type itemType);
        IContainerItem[] GetEquippedItems();

        int GetItemAmount(Type itemType);
        bool TryToAdd(object sender, IContainerItem item);
        void Remove(object sender, Type itemType, int amount = 1);
        bool HasItem(Type type, out IContainerItem containerItem);
    }
}