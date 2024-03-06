using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Entities
{
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float speed;

        public Vector2 Position => transform.position;

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
        
        private void OnGUI()
        {
            Debug.DrawLine(transform.position, debugTargetPosition);
        }

        public Coroutine GoTo(Transform targetTransform, CancellationTokenSource tokenSource) =>
            StartCoroutine(GoToCoroutine(targetTransform, tokenSource));
        
        private IEnumerator GoToCoroutine(Transform targetTransform, CancellationTokenSource tokenSource)
        {
            var completed = false;
            while (!completed && !tokenSource.IsCancellationRequested)
            {
                if (targetTransform == null) yield break;
                Move(targetTransform.position, out completed);
                yield return 0;
            }
        }
    }
}