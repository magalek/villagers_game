using System;
using Movement;
using UnityEngine;

namespace Entities
{
    public class EntityMovement : MonoBehaviour, IMovement
    {
        [SerializeField, Range(0f, 10f)] private float speed;

        public Vector2 Position => transform.position;

        public float Move(Vector2 targetPosition, out bool completed)
        {
            var position = (Vector2)gameObject.transform.position;
            var distance = targetPosition - position;
            var dir = distance.normalized;
            position += dir * speed * Time.deltaTime;
            transform.position = new Vector3(position.x, position.y, position.y);
            completed = distance.sqrMagnitude <= 0.001f;
            return distance.magnitude;
        }

        public float Move(IMovementTarget target, out bool completed) => Move(target.GetPosition(), out completed);
    }
}