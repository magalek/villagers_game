using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Items
{
    public static class ItemsExtension
    {
        public static bool Contains(this List<Item> items, Item comparedItem)
        {
            var result = items.FirstOrDefault(item => item.Id == comparedItem.Id);
            return result != null;
        }
        
        public static bool Contains(this List<Item> items, Item comparedItem, out int index)
        {
            index = -1;
            var itemsList = items.ToList();
            var result = itemsList.FirstOrDefault(item => item.Id == comparedItem.Id);
            var contains = result != null;
            if (contains) index = itemsList.IndexOf(result);
            return contains;
        }
        
        public static bool Contains(this IReadOnlyList<Item> items, Item comparedItem)
        {
            var result = items.FirstOrDefault(item => item.Id == comparedItem.Id);
            return result != null;
        }
        
        public static bool Contains(this IReadOnlyList<Item> items, Item comparedItem, out int index)
        {
            index = -1;
            var itemsList = items.ToList();
            var result = itemsList.FirstOrDefault(item => item.Id == comparedItem.Id);
            if (result != null) index = itemsList.IndexOf(result);
            return result != null;
        }
    }
}