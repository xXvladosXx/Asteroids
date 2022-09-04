using FluentAssertions;
using InventorySystem;
using InventorySystem.Core;
using InventorySystem.Items;
using NUnit.Framework;

namespace Tests.InventorySystem
{
    public class InventoryTests
    {
        [Test]
        public void WhenStoringItem_AndInventoryIsEmpty_ThenItemCountShouldBe1()
        {
            // Arrange.
            IContainer container = new InventoryContainer(10);

            // Act.
            container.TryToAdd(this, new Bow(1));

            // Assert.
            container.GetAllItems().Length.Should().Be(1);
        }
        
        [Test]
        public void WhenStoringItem_AndInventoryWithoutSlots_ThenShouldBeFalse()
        {
            // Arrange.
            IContainer container = new InventoryContainer(0);

            // Act.
            container.TryToAdd(this, new Bow(1));

            // Assert.
            container.GetAllItems().Length.Should().Be(0);
        }
        
        [Test]
        public void WhenStoringItem_AndInventoryIsFull_ThenShouldBeFalse()
        {
            // Arrange.
            IContainer container = new InventoryContainer(0);

            // Act.
            container.TryToAdd(this, new Bow(1)).Should().Be(false);

            // Assert.
        }

        [Test]
        public void WhenCheckingItem_AndAddingSword_ThenShouldBeFoundSword()
        {
            // Arrange.
            IContainer container = new InventoryContainer(2);

            // Act.
            container.TryToAdd(this, new Sword(1));

            // Assert.
            container.HasItem(typeof(Sword)).Should().Be(true);
        }
        
        [Test]
        public void WhenStoringItem_AndInventoryIsEmpty_ThenShouldBeFullContainer()
        {
            // Arrange.
            IContainer container = new InventoryContainer(3);

            // Act.
            for (int i = 0; i <= container.Capacity; i++)
            {
                container.TryToAdd(this, new Sword(1, 1));
            }

            // Assert.
            container.IsFull.Should().Be(true);
        }
        
        [Test]
        public void WhenSearchingItem_AndInventoryHasItem_ThenAmountOfFoundItemsShouldBe5()
        {
            // Arrange.
            IContainer container = new InventoryContainer(5);

            // Act.
            container.TryToAdd(this, new Sword(1, 5));

            // Assert.
            container.GetItemAmount(typeof(Sword)).Should().Be(5);
        }
        
        [Test]
        public void WhenRemovingItem_AndItemWasAdded_ThenAmountOfItemsShouldBe0()
        {
            // Arrange.
            IContainer container = new InventoryContainer(5);

            // Act.
            container.TryToAdd(this, new Sword(1, 5));
            container.Remove(this, typeof(Sword), 5);

            // Assert.
            container.GetAllItems().Should().BeEmpty();
        }
        
        [Test]
        public void WhenRemoving1Item_AndItemWasAddedInAmountOf5_ThenAmountOfItemsShouldBe4()
        {
            // Arrange.
            IContainer container = new InventoryContainer(5);

            // Act.
            container.TryToAdd(this, new Sword(1, 5));
            container.Remove(this, typeof(Sword));

            // Assert.
            container.GetAllItems().Length.Should().Be(4);
        }
        
        [Test]
        public void WhenStoringItem_AndEquippingItem_ThenAmountOfEquippedItemsShouldBe1()
        {
            // Arrange.
            IContainer container = new InventoryContainer(5);

            // Act.
            container.TryToAdd(this, new Sword(1, 1));
            container.GetItem(typeof(Sword)).IsEquipped = true;

            // Assert.
            container.GetEquippedItems().Length.Should().Be(1);
        }
    }
}