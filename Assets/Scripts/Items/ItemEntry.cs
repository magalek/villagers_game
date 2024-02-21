using System;

namespace Items
{
    public class ItemEntry
    {
        public readonly Item item;
        public int amount;

        public ItemEntry(Item item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }

        public ItemEntry(ItemEntry entry)
        {
            item = entry.item;
            amount = entry.amount;
        }

        public override string ToString() => $"{item.Id} | {item.name} - {amount}";

        public bool Equals(ItemEntry entry) => item.Id == entry.item.Id;
    }
}