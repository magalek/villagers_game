using System.Collections.Generic;
using Items;
using Utility;

namespace Interfaces
{
    public interface IInputNode : IActionNode
    {
        void Add(Item item);

        bool Accepts(Item item);
        
        IItemContainer Container { get; }
    }
}