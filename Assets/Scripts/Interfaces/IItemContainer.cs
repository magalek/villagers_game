using System;

namespace Interfaces
{
    public interface IItemContainer : IContainer
    {
        event Action<IItem> ItemAdded;
        event Action<IItem> ItemRemoved;
        
        bool TryAddItem(IItem item);

        IItem TryRemoveItem(IItem item);

        void DropItem(IItem item);
        void DropItem();
    }
}