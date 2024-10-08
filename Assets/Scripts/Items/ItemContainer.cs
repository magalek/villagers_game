﻿using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Items
{
    public class ItemContainer : MonoBehaviour, IItemContainer
    {
        public event Action Updated
        {
            add => items.Updated += value;
            remove => items.Updated -= value;
        }

        public event Action<Item> ItemAdded
        {
            add => items.ItemAdded += value;
            remove => items.ItemAdded -= value;
        }

        public event Action<Item> ItemRemoved
        {
            add => items.ItemRemoved += value;
            remove => items.ItemRemoved -= value;
        }
        
        public IReadOnlyList<ContainerEntry> Items => items.Items;

        private ItemDictionary items = new ItemDictionary();

        public void AddItem(ContainerEntry entry) => items.Add(entry);

        public void RemoveItem(ContainerEntry entry) => items.Subtract(entry);
        public IEnumerable<ContainerEntry> RemoveAll() => Items;

        public bool HasItem(Item item) => items.Contains(item);
        public int Count(Item item) => items.Count(item);
    }
}