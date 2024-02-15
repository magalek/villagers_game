using System;
using System.Collections.Generic;
using UI;

namespace Interfaces
{
    public interface IItemContainer : IContainer, IUIPanelBuilder
    {
        event Action<IItem> ItemAdded;
        event Action<IItem> ItemRemoved;

        IReadOnlyList<IItem> Items { get; }

        bool TryAddItem(IItem item, int amount = 1);

        IItem TryRemoveItem(IItem item);

        void DropItem(IItem item);
        void DropItem();

        bool HasItem(IItem item);
    }
}