using Entities;
using Interfaces;
using UnityEngine;

namespace Movement
{
    public struct MoveDestination
    {
        public readonly Vector2 Position;
        public readonly IMoveTarget Target;

        public MoveDestination(Vector2 position)
        {
            Position = position;
            Target = null;
        }
        
        public MoveDestination(IMoveTarget target)
        {
            Position = target.transform.position;
            Target = target;
        }

        public void Complete(IEntity entity) => Target?.OnReachedTarget(entity);
    }
}