using System;

namespace Entities
{
    public interface IActionProgress
    {
        event Action Ended;
        
        float PercentageNormalized { get; }
        
        bool IsCompleted { get; }
    }
}