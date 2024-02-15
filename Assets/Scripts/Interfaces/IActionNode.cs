using System;
using System.Collections.Generic;
using Actions;
using Entities;
using UnityEngine;

namespace Interfaces
{
    public interface IActionNode<TData> : IActionNode
        where TData : IActionNodeContext
    {
        event Action<TData> Changed;
    }

    public interface IActionNode
    {
        Vector2 Position { get; }
        ActionType ActionType { get; }
        IEnumerable<IAction> GetActions(IEntity worker);
        bool CanBeUsedBy(IEntity worker);
    }
}