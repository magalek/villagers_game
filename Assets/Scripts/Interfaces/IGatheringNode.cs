using Entities;
using Items;
using Targets;
using Utility;

namespace Interfaces
{
    public interface IGatheringNode : IActionNode<GatheringNodeContext>
    {
        IEntity CurrentWorker { get; }
        
        float GatheringTime { get; }
        ItemEntry GatherItem(int harvestAmount);
        ComponentGetter<IItemContainer> Container { get; }
    }
}