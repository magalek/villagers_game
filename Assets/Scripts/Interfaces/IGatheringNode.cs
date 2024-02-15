using Entities;
using Targets;
using Utility;

namespace Interfaces
{
    public interface IGatheringNode : IActionNode<GatheringNodeContext>
    {
        IEntity CurrentWorker { get; }
        
        float GatheringTime { get; }
        IItem GatherItem();
        ComponentGetter<IItemContainer> Container { get; }
    }
}