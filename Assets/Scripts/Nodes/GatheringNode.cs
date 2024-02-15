using System;
using System.Collections.Generic;
using Actions;
using Entities;
using Interfaces;
using Items;
using Map.Tiles;
using Targets;
using UI;
using UnityEngine;
using Utility;

namespace Nodes
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GatheringNode : ActionNodeBase, IGatheringNode
    {
        [SerializeField] private Item gatheredItem;
        [SerializeField] private int amount;
        
        public IEntity CurrentWorker { get; private set; }
        
        public ActionType ActionType { get; }
        public event Action<GatheringNodeContext> Changed;
        public bool IsUsed => CurrentWorker != null;
        public Vector2 Position => transform.position;
        public float GatheringTime { get; } = 2;

        public ComponentGetter<IItemContainer> Container { get; private set; }
        public override UIEventHandler UIEventHandler { get; protected set; }

        protected override void Awake()
         {
             base.Awake();
             Container = new ComponentGetter<IItemContainer>(this);
             Container.Get().TryAddItem(gatheredItem, amount);
             UIEventHandler = new ContainerUIEventHandler(Container.Get());
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

        public bool CanBeUsedBy(IEntity worker) => CurrentWorker == null;

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
            Changed?.Invoke(new GatheringNodeContext {gathered = true});
        }
    }
}