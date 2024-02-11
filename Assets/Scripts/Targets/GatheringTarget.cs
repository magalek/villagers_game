using System;
using System.Collections.Generic;
using Actions;
using Entities;
using Interfaces;
using Items;
using UnityEngine;
using Action = System.Action;

namespace Targets
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GatheringTarget : ActionTargetBase, IGatheringTarget
    {
        [SerializeField] private Item gatheredItem;
        
        public IEntity CurrentWorker { get; private set; }
        
        public ActionType ActionType { get; }

        public event Action<GatheringTargetData> Changed;
        public bool IsUsed => CurrentWorker != null;
        public Vector2 Position => transform.position;
        public float GatheringTime { get; } = 2;

        public int amount;

         protected override void Awake()
         {
             base.Awake();
             amount = 5;
         }

         public IItem GatherItem()
        {
            amount -= 1;
            Debug.Log("gathered new item");
            CurrentWorker = null;
            if (amount <= 0) OnGathered();
            return gatheredItem.Copy();
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

        private void OnDestroy()
        {
            Changed?.Invoke(new GatheringTargetData {gathered = true});
        }
    }
}