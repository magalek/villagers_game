using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IItemContainer : IContainer
    {
        event Action<IItem> ItemAdded;
        event Action<IItem> ItemRemoved;

        IReadOnlyList<IItem> Items { get; }

        bool TryAddItem(IItem item);

        IItem TryRemoveItem(IItem item);

        void DropItem(IItem item);
        void DropItem();

        bool HasItem(IItem item);
    }
}