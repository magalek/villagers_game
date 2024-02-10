using UnityEngine;

namespace Movement
{
    public interface IMovement
    {
        Vector2 Position { get; }
        float Move(Vector2 targetPosition, out bool completed);
    }
}