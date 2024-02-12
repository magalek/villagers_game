using System.Collections;
using System.Threading.Tasks;
using Entities;
using Managers;
using Movement;
using UnityEngine;

namespace Actions
{
    public class MoveAction : Action
    {
        public override bool ShowProgress => false;

        private readonly Vector2 targetPosition;

        private IMovement workerMovement;
        private float magnitude;
        
        public MoveAction(Vector2 targetPosition)
        {
            this.targetPosition = targetPosition;
            workerMovement = null;
        }
        
        protected override void OnStarted(Entity entity)
        {
            workerMovement = entity.Movement.Get();
            magnitude = (targetPosition - workerMovement.Position).magnitude;
            entity.StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            while (!progress.IsCompleted)
            {
                //Debug.Log("in moving task");
                yield return 0;
                MoveWorker();
            }
            OnMovementEnded();
        }

        private void MoveWorker()
        {
            var distanceMagnitude = workerMovement.Move(targetPosition, out bool completed);
            if (completed)
            {
                progress.Complete();
                return;
            }
            var currentProgress = (magnitude - distanceMagnitude) / magnitude;
            progress.Update(currentProgress);
        }

        protected virtual void OnMovementEnded() {}
    }
}