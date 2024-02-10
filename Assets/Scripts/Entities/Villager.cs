using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Actions;
using Movement;
using Utility;

namespace Entities
{
    public class Villager : Entity
    {
        public float gatheringSpeed = 1;
    }

    public abstract class Entity : MonoBehaviour, IEntity, IProcessable
    {
        protected Queue<IAction> ScheduledWork = new Queue<IAction>();

        protected IAction currentAction;

        public ComponentGetter<IMovement> Movement { get; private set; }

        protected void Awake()
        {
            Movement = new ComponentGetter<IMovement>(this);
            currentAction = new IdleAction();
            Debug.Log(currentAction);
        }

        protected void Start()
        {
            ManagerLoader.Get<UpdateManager>().RegisterProcessable(this);
            ScheduledWork.Enqueue(new MoveAction(Vector2.right * 5));
            Debug.Log(currentAction);
        }

        public virtual void Process()
        {
            if (!currentAction.Progress.IsCompleted) return;
            Debug.Log(currentAction);

            if (ScheduledWork.TryDequeue(out IAction work))
            {
                currentAction = work;
                currentAction.Start(this);
            }
            else currentAction = new IdleAction();
        }

    }

    public interface IProcessable
    {
        void Process();
    }
}