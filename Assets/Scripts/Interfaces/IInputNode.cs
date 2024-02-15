using Utility;

namespace Interfaces
{
    public interface IInputNode : IActionNode
    {
        void Add(IItem item);

        bool Accepts(IItem item);
        
        ComponentGetter<IItemContainer> Container { get; }
    }
}