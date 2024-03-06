using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        bool TryGetAction(IEntity worker, out EntityAction action);

        IEnumerator UseCoroutine(ActionData data, CancellationTokenSource cancellationTokenSource);

        void Lock(IEntity entity);
    }

    public interface IPlaceable 
    {
        event Action<IPlaceable> WillDestroy;

        new Transform transform { get; }

        // ReSharper disable once InconsistentNaming
        GameObject gameObject { get; }
    }
}