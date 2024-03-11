using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items
{
    public class ItemDictionary
    {
        public event Action Updated;
        public event Action<Item> ItemAdded; 
        public event Action<Item> ItemRemoved; 
        
        public IReadOnlyList<ContainerEntry> Items => itemEntries.Values.ToList();
        
        private Dictionary<string, ContainerEntry> itemEntries = new Dictionary<string, ContainerEntry>();

        public void Add(ContainerEntry itemEntry)
        {
            if (itemEntry.item == null) return;
            if (itemEntries.TryGetValue(itemEntry.item.Id, out var entry))
            {
                entry.amount += itemEntry.amount;
            }
            else
            {
                itemEntries[itemEntry.item.Id] = new ContainerEntry(itemEntry.item, itemEntry.amount);
                ItemAdded?.Invoke(itemEntry.item);
            }

            Updated?.Invoke();
        }

        public void Subtract(ContainerEntry itemEntry)
        {
            if (itemEntries.TryGetValue(itemEntry.item.Id, out var entry))
            {
                entry.amount -= itemEntry.amount;
                if (entry.amount <= 0)
                {
                    itemEntries.Remove(itemEntry.item.Id);
                    ItemRemoved?.Invoke(itemEntry.item);
                }
            }
            Updated?.Invoke();
        }

        public bool Contains(Item item) => itemEntries.ContainsKey(item.Id);

        public int Count(Item item) => itemEntries[item.Id].amount;
    }
}