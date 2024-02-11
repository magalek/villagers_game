using System.Collections.Generic;
using Actions;
using Entities;
using Interfaces;
using UnityEngine;

namespace Targets
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GatheringTarget : MonoBehaviour, IGatheringTarget
    {
        public IEntity CurrentWorker { get; private set; }
        
        public ActionType ActionType { get; }

        public bool IsUsed => CurrentWorker != null;
        public Vector2 Position => transform.position;
        public float GatheringTime { get; } = 2;

        public int amount;

        private void Awake()
        {
            amount = 5;
        }

        public IItem Gather()
        {
            amount -= 1;
            Debug.Log("gathered new item");
            CurrentWorker = null;
            if (amount <= 0) OnGathered();
            return null;
        }

        protected virtual void OnGathered()
        {
            Destroy(gameObject);
        }

        public bool TryGetActions(IEntity worker, out Queue<IAction> actions)
        {
            actions = new Queue<IAction>();
            if (CurrentWorker != null) return false;
            actions.Enqueue(new MoveAction(transform.position));
            actions.Enqueue(new GatheringAction(this));
            CurrentWorker = worker;
            return true;
        }
    }
}