using Actions;
using Managers;
using Movement;
using UnityEngine;
using Utility;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IEntity, IProcessable
    {
        public ActionQueue ActionQueue { get; private set; }
        
        public ComponentGetter<IMovement> Movement { get; private set; }

        protected void Awake()
        {
            ActionQueue = new ActionQueue();
            Movement = new ComponentGetter<IMovement>(this);
            Debug.Log(ActionQueue.currentAction);
        }

        protected void Start()
        {
            ManagerLoader.Get<ProgressManager>().RegisterProgressBar(ActionQueue, transform);
            ManagerLoader.Get<UpdateManager>().RegisterProcessable(this);
        }

        public virtual void Process()
        {
            if (!ActionQueue.currentAction.Progress.IsCompleted) return;
            if (ActionQueue.currentAction is IdleAction) SearchForWork();

            if (ActionQueue.GetNextAction(out ActionQueue.currentAction)) ActionQueue.currentAction.Start(this);
            else ActionQueue.currentAction = new IdleAction();
        }

        private void SearchForWork()
        {
            var target = ManagerLoader.Get<TargetManager>().GetNearestTarget(transform.position, 10);
            if (target == null) return;
            if (target.TryGetActions(this, out var actions)) ActionQueue.AddActions(actions);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 10);
        }
    }
}