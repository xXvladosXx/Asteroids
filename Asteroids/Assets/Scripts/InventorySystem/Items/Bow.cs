using System;
using InventorySystem.Core;

namespace InventorySystem.Items
{
    public class Bow : IContainerItem
    {
        public bool IsEquipped { get; set; }
        public Type Type => GetType();
        public int MaxItemsInStack { get; }
        public int Amount { get; set; }

        public Bow(int maxItemsInStack)
        {
            MaxItemsInStack = maxItemsInStack;
        }

        public IContainerItem Clone() => new Sword(MaxItemsInStack) {Amount = Amount};
    }
}