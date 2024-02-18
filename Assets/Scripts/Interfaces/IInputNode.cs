using System.Collections.Generic;
using Items;
using Utility;

namespace Interfaces
{
    public interface IInputNode : IActionNode
    {
        void Add(IEnumerable<ItemEntry> entries);

        bool Accepts(IItem item);
        
        ComponentGetter<IItemContainer> Container { get; }
    }
}