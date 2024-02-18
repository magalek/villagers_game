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
        
        public IReadOnlyList<ItemEntry> Items => itemEntries.Values.ToList();
        
        private Dictionary<string, ItemEntry> itemEntries = new Dictionary<string, ItemEntry>();

        public void Add(ItemEntry itemEntry)
        {
            if (itemEntries.TryGetValue(itemEntry.item.Id, out var entry))
            {
                entry.amount += itemEntry.amount;
            }
            else
            {
                itemEntries[itemEntry.item.Id] = new ItemEntry(itemEntry);
                ItemAdded?.Invoke(itemEntry.item);
            }

            Updated?.Invoke();
        }

        public void Subtract(ItemEntry itemEntry)
        {
            if (itemEntries.TryGetValue(itemEntry.item.Id, out var entry))
            {
                entry.amount -= itemEntry.amount;
                if (entry.amount < 0)
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