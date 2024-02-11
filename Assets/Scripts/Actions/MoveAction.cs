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
        private readonly Vector2 targetPosition;
        
        private readonly UpdateManager updateManager;
        
        private IMovement workerMovement;
        private float magnitude;
        
        public MoveAction(Vector2 targetPosition)
        {
            this.targetPosition = targetPosition;

            updateManager = ManagerLoader.Get<UpdateManager>();
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
                Debug.Log("in moving task");
                yield return 0;
                MoveWorker();
            }
            
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