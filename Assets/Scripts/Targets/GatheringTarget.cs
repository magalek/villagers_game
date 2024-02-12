using System;
using System.Collections.Generic;
using Actions;
using Entities;
using Interfaces;
using Items;
using Map.Tiles;
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

         public override void OnClick(MapTile tile)
         {
             
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

        public bool CanUse(IEntity worker) => CurrentWorker == null;

        public IEnumerable<IAction> GetActions(IEntity worker)
        {
            var actions = new Queue<IAction>();
            actions.Enqueue(new MoveAction(transform.position));
            actions.Enqueue(new GatheringAction(this));
            CurrentWorker = worker;
            return actions;
        }

        private void OnDestroy()
        {
            Changed?.Invoke(new GatheringTargetData {gathered = true});
        }
    }
}