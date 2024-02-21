using Entities;
using UnityEngine;

namespace Interfaces
{
    public interface IMoveTarget
    {
        // ReSharper disable once InconsistentNaming
        Transform transform { get; }
        void OnReachedTarget(IEntity entity);
    }
}