using System;
using InventorySystem.Core;

namespace InventorySystem.Items
{
    public class Sword : IContainerItem
    {
        public bool IsEquipped { get; set; }
        public Type Type => GetType();
        public int MaxItemsInStack { get; }
        public int Amount { get; set; }

        public Sword(int maxItemsInStack)
        {
            MaxItemsInStack = maxItemsInStack;
        }
        
        public Sword(int maxItemsInStack, int amount)
        {
            MaxItemsInStack = maxItemsInStack;
            Amount = amount;    
        }

        public IContainerItem Clone() => new Sword(MaxItemsInStack) {Amount = Amount};
    }
}