using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using Interfaces;
using Managers;
using Movement;
using UnityEngine;

namespace Actions
{
    public class MoveAction : Action
    {
        public override bool ShowProgress => false;

        private readonly Queue<MoveDestination> targetQueue = new Queue<MoveDestination>();

        protected Entity worker;
        private IMovement workerMovement;
        private float magnitude;

        private MoveDestination currentTarget;

        protected MoveAction(Vector2 position)
        {
            targetQueue.Enqueue(new MoveDestination(position));
            workerMovement = null;
        }
        
        protected MoveAction(IMoveTarget target)
        {
            if (target?.transform == null )
            {
                Cancel();
                return;
            }
            targetQueue.Enqueue(new MoveDestination(target));
            workerMovement = null;
        }
        
        protected MoveAction(IMoveTarget target1, IMoveTarget target2)
        {
            if (target1?.transform == null || target2?.transform == null)
            {
                Cancel();
                return;
            }
            targetQueue.Enqueue(new MoveDestination(target1));
            targetQueue.Enqueue(new MoveDestination(target2));
            workerMovement = null;
        }

        protected MoveAction(IEnumerable<IMoveTarget> targets)
        {
            foreach (var target in targets)
            {
                if (target?.transform == null)
                {
                    Cancel();
                    return;
                }
                targetQueue.Enqueue(new MoveDestination(target));
            }
            workerMovement = null;
        }
        
        protected override void OnStarted(Entity entity)
        {
            worker = entity;
            workerMovement = entity.Movement.Get();
            entity.StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            cancelationToken = new ActionCancelationToken();
            while (targetQueue.Count > 0)
            {
                progress.Reset();
                currentTarget = targetQueue.Dequeue();
                magnitude = (currentTarget.Position - workerMovement.Position).magnitude;
                while (!progress.IsMax)
                {
                    yield return 0;
                    if (cancelationToken!.canceled)
                    {
                        cancelationToken = null;
                        yield break;
                    }
                    MoveWorker();
                }
                if (cancelationToken.canceled) yield break;

                var routine = OnTargetReached(currentTarget);
                if (routine != null)
                {
                    Debug.Log("started target coroutine");
                    yield return worker.StartCoroutine(routine);
                    Debug.Log("ended target coroutine");
                }
            }
            progress.Complete();
        }

        private void MoveWorker()
        {
            var distanceMagnitude = workerMovement.Move(currentTarget, out bool completed);
            if (completed)
            {
                progress.Clamp();
                return;
            }
            var currentProgress = (magnitude - distanceMagnitude) / magnitude;
            progress.Update(currentProgress);
        }

        protected virtual IEnumerator OnTargetReached(MoveDestination destination)
        {
            Debug.Log($"Reached {destination.Target.transform.name}");
            destination.Target.OnReachedTarget(worker);
            return null;
        }
    }
}