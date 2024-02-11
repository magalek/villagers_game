using Utility;

namespace Interfaces
{
    public interface IInputTarget : IActionTarget
    {
        void Add(IItem item);

        bool Accepts(IItem item);
        
        ComponentGetter<IItemContainer> Container { get; }
    }
}