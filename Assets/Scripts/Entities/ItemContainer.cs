﻿using System;
using System.Collections.Generic;
using Interfaces;
using Items;
using Unity.VisualScripting;
using UnityEngine;

namespace Entities
{
    public class ItemContainer : MonoBehaviour, IItemContainer
    {
        public event Action<IItem> ItemAdded;
        public event Action<IItem> ItemRemoved;

        private List<IItem> items = new List<IItem>();

        public IReadOnlyList<IItem> Items => items;

        public bool TryAddItem(IItem item, int amount = 1)
        {
            if (items.Contains(item, out int index))
            {
                items[index].ChangeAmount(amount);
            }
            else
            {
                item.ChangeAmount(amount);
                items.Add(item);
            }
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