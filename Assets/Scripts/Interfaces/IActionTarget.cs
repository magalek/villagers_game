using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Interfaces
{
    public interface IActionTarget
    {
        bool IsUsed { get; }
        Vector2 Position { get; }
        ActionType ActionType { get; }
        bool TryGetActions(IEntity worker, out Queue<IAction> actions);
    }
}