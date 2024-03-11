using System;
using Movement;
using UnityEngine;

namespace Entities
{
    public class EntityMovement : MonoBehaviour, IMovement
    {
        [SerializeField, Range(0f, 10f)] private float speed;

        private Vector2 debugTargetPosition;

        public float Move(Vector2 targetPosition, out bool completed)
        {
            debugTargetPosition = targetPosition;
            var position = (Vector2)gameObject.transform.position;
            var distance = targetPosition - position;
            var dir = distance.normalized;
            completed = distance.sqrMagnitude <= 0.001f;
            if (completed) return 0;
            position += dir * speed * Time.deltaTime;
            transform.position = new Vector3(position.x, position.y, position.y);
            completed = distance.sqrMagnitude <= 0.001f;
            return distance.magnitude;
        }

        public float Move(MoveDestination destination, out bool completed) => Move(destination.Position, out completed);

        private void OnGUI()
        {
            Debug.DrawLine(transform.position, debugTargetPosition);
        }
    }
}