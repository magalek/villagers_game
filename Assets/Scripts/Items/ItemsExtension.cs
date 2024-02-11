using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Items
{
    public static class ItemsExtension
    {
        public static bool IsEqual(this IItem item, IItem comparedItem)
        {
            return item.Id == comparedItem.Id;
        }

        public static bool Contains(this IEnumerable<IItem> items, IItem comparedItem)
        {
            var result = items.FirstOrDefault(item => item.Id == comparedItem.Id);
            return result != null;
        }
    }
}