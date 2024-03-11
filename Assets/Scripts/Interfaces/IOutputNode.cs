using Items;

namespace Interfaces
{
    public interface IOutputNode : IActionNode
    {
        public abstract bool ContainsItem(Item _item);
    }
}