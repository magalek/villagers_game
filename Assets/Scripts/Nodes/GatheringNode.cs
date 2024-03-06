using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Actions;
using Entities;
using Interfaces;
using Items;
using Managers;
using Map.Tiles;
using Targets;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace Nodes
{
    public class GatheringNode : ActionNodeBase
    {
        [SerializeField] private Item gatheredItem;
        [SerializeField] private int startAmount;
        
        public IEntity CurrentWorker { get; private set; }
        
        public override ActionType ActionType { get; }

        public float GatheringTime { get; } = 2;

        public IItemContainer Container { get; private set; }
        public override UIEventHandler UIEventHandler { get; protected set; }

        private float currentGatheringTime;

        protected override void Awake()
        {
             base.Awake();
             Container = GetComponentInChildren<IItemContainer>();
             Container.AddItem(new ContainerEntry(gatheredItem, startAmount));
             UIEventHandler = new ContainerUIEventHandler(Container);
        }

        public override bool TryGetAction(IEntity worker, out EntityAction action)
        {
            action = new EntityAction(this);
            return lockerEntity == null;
        }

        public override IEnumerator UseCoroutine(ActionData data, CancellationTokenSource cancellationTokenSource)
        {
            CurrentWorker = data.Entity;
            while (currentGatheringTime < GatheringTime)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    OnEnded();
                    yield break;
                }
                yield return 0;
                currentGatheringTime += Time.deltaTime;
            }
            CompleteGather();
        }

        private void CompleteGather()
        {
            Container.RemoveItem(new ContainerEntry(gatheredItem, 1));
            ItemManager.Current.SpawnItemObject(gatheredItem, 1, transform.position);
            currentGatheringTime = 0;
            OnEnded();
        }

        private void OnEnded()
        {
            CurrentWorker = null;
            if (Container.Items.Count == 0)
            {
                DestroyNode();
            }
        }
    }
}