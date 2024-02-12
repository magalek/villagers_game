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
        Vector2 Position { get; }
        ActionType ActionType { get; }
        IEnumerable<IAction> GetActions(IEntity worker);
        bool CanUse(IEntity worker);
    }
    
    public interface IActionTargetData
    {
        
    }
}