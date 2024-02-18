using System;
using System.Collections.Generic;
using Items;
using UI;

namespace Interfaces
{
    public interface IItemContainer : IContainer, IUIPanelBuilder
    {
        event Action Updated;
        event Action<Item> ItemAdded;
        event Action<Item> ItemRemoved;

        IReadOnlyList<ItemEntry> Items { get; }

        void AddItem(ItemEntry entry);

        void RemoveItem(ItemEntry entry);

        IEnumerable<ItemEntry> RemoveAll();

        bool HasItem(Item item);

        int Count(Item item);
    }
}