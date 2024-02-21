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
using UnityEngine.Serialization;
using Utility;

namespace Nodes
{
    public class GatheringNode : ActionNodeBase, IGatheringNode
    {
        [SerializeField] private Item gatheredItem;
        [SerializeField] private int startAmount;
        
        public IEntity CurrentWorker { get; private set; }
        
        public override ActionType ActionType { get; }
        public event Action<GatheringNodeContext> Changed;
        public bool IsUsed => CurrentWorker != null;

        public float GatheringTime { get; } = 2;

        public ComponentGetter<IItemContainer> Container { get; private set; }
        public override UIEventHandler UIEventHandler { get; protected set; }

        private ItemEntry item;

        protected override void Awake()
        {
             base.Awake();
             item = new ItemEntry(gatheredItem, startAmount);
             Container = new ComponentGetter<IItemContainer>(this);
             Container.Get().AddItem(item);
             UIEventHandler = new ContainerUIEventHandler(Container.Get());
        }

        public ItemEntry GatherItem(int harvestAmount)
        {
            item.amount -= harvestAmount;
            CurrentWorker = null;
            if (item.amount <= 0) OnGathered();
            return new ItemEntry(gatheredItem, harvestAmount);
        }

        protected virtual void OnGathered()
        {
            Destroy(gameObject);
        }
        
        public override bool TryGetActions(IEntity worker, out List<IAction> actions)
        {
            actions = new List<IAction>();
            if (CurrentWorker != null)
            {
                return false;
            }
            actions.Add(new GatheringAction(this));
            CurrentWorker = worker;
            return true;
        }

        public override void OnReachedTarget(IEntity entity)
        {

        }

        private void OnDestroy()
        {
            Changed?.Invoke(new GatheringNodeContext {gathered = true});
            OnDestroyed();
        }
    }
}