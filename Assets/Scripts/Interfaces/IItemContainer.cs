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

        IReadOnlyList<ContainerEntry> Items { get; }

        void AddItem(ContainerEntry entry);

        void RemoveItem(ContainerEntry entry);

        IEnumerable<ContainerEntry> RemoveAll();

        bool HasItem(Item item);

        int Count(Item item);
    }
}