using System;
using System.Collections.Generic;
using Actions;
using Entities;
using UnityEngine;

namespace Interfaces
{
    public interface IActionTarget<TData> : IActionTarget
        where TData : IActionTargetData
    {
        event Action<TData> Changed;
    }

    public interface IActionTarget
    {
        bool IsUsed { get; }
        Vector2 Position { get; }
        ActionType ActionType { get; }
        bool TryGetActions(IEntity worker, out Queue<IAction> actions);
    }
    
    public interface IActionTargetData
    {
        
    }
}