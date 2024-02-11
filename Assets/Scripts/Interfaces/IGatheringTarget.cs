using Entities;

namespace Interfaces
{
    public interface IGatheringTarget : IActionTarget
    {
        IEntity CurrentWorker { get; }
        
        float GatheringTime { get; }
        IItem Gather();
    }
}