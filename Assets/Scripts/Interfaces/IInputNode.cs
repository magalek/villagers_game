using System.Collections.Generic;
using Items;
using Utility;

namespace Interfaces
{
    public interface IInputNode : IActionNode
    {
        void Add(IEnumerable<ItemEntry> entries);

        bool Accepts(Item item);
        
        ComponentGetter<IItemContainer> Container { get; }
    }

    public interface IOutputNode : IActionNode
    {
        bool ContainsItem(ItemEntry itemEntry);
        bool ContainsItem(Item item);
        ItemEntry GetItem();
    }
}