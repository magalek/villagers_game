using Entities;
using Targets;

namespace Interfaces
{
    public interface IGatheringTarget : IActionTarget<GatheringTargetData>
    {
        IEntity CurrentWorker { get; }
        
        float GatheringTime { get; }
        IItem GatherItem();
    }
}