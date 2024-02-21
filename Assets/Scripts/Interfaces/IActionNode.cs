using System;
using System.Collections.Generic;
using Actions;
using Entities;
using UnityEngine;
using Action = System.Action;

namespace Interfaces
{
    public interface IActionNode<TData> : IActionNode
        where TData : IActionNodeContext
    {
        event Action<TData> Changed;
    }

    public interface IActionNode : IPlaceable 
    {
        ActionType ActionType { get; }
        bool TryGetActions(IEntity worker, out List<IAction> actions);
    }

    public interface IPlaceable : IMoveTarget
    {
        event Action<IPlaceable> Destroyed;

        new Transform transform { get; }

        // ReSharper disable once InconsistentNaming
        GameObject gameObject { get; }
    }
}