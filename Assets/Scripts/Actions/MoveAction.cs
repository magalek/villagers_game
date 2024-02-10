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
        
        public MoveAction(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;

            updateManager = ManagerLoader.Get<UpdateManager>();
            workerMovement = null;
        }
        
        public override void Start(Entity entity)
        {
            Debug.Log("in move start");
            workerMovement = entity.Movement.Get();
            magnitude = (targetPosition - workerMovement.Position).magnitude;
            entity.StartCoroutine(MoveToCoroutine());
        }

        private IEnumerator MoveToCoroutine()
        {
            while (!progress.IsCompleted)
            {
                Debug.Log("in task");
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
    }
}