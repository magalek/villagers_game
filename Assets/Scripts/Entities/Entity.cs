﻿using System;
using Actions;
using Interfaces;
using Items;
using Managers;
using Movement;
using UnityEngine;
using Utility;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IEntity, IProcessable
    {
        public ActionQueue ActionQueue { get; private set; }
        public EntityStatistics Statistics => statistics;

        public ComponentGetter<IMovement> Movement { get; private set; }
        public ComponentGetter<IItemContainer> Container { get; private set; }
        public ComponentGetter<ItemHolder> ItemHolder { get; private set; }

        [SerializeField] private EntityStatistics statistics = new EntityStatistics();
        
        private IAction CurrentAction => ActionQueue.currentAction;

        protected void Awake()
        {
            ActionQueue = new ActionQueue();
            Movement = new ComponentGetter<IMovement>(this);
            Container = new ComponentGetter<IItemContainer>(this);
            ItemHolder = new ComponentGetter<ItemHolder>(this);
        }

        protected void Start()
        {
            ProgressManager.Current.RegisterProgressBar(ActionQueue, transform);
            MonoBehaviourManager.Current.RegisterProcessable(this);
        }

        public virtual void Process()
        {
            if (CurrentAction.InProgress) return;
            if (CurrentAction is IdleAction) SearchForWork();

            if (ActionQueue.GetNextAction(out ActionQueue.currentAction)) CurrentAction.Start(this);
            else ActionQueue.currentAction = new IdleAction();
        }

        private void SearchForWork()
        {
            var target = NodeHelper.GetNearestNode(this);
            if (target == null) return;
            ActionQueue.AddActions(target.GetActions(this));
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 10);
        }
    }
}