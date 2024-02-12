using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Entities
{
    public class ItemContainer : MonoBehaviour, IItemContainer
    {
        public event Action<IItem> ItemAdded;
        public event Action<IItem> ItemRemoved;

        private List<IItem> items = new List<IItem>();

        public IReadOnlyList<IItem> Items => items;

        public bool TryAddItem(IItem item)
        {
            items.Add(item);
            return true; // change so if container full returns false
        }

        public IItem TryRemoveItem(IItem item)
        {
            items.Remove(item);
            return item.Copy();
        }

        public void DropItem(IItem item)
        {
            throw new NotImplementedException();
        }

        public void DropItem()
        {
            throw new NotImplementedException();
        }

        public bool HasItem(IItem item)
        {
            throw new NotImplementedException();
        }
    }

    public class EquipmentContainer
    {
        
    }
}