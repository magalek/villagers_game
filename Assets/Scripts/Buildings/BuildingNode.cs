using System;
using System.Collections;
using System.Threading;
using Actions;
using Data.Buildings;
using Entities;
using Interfaces;
using Items;
using Nodes;
using UI;
using UnityEngine;
using Actions;

namespace Buildings
{
    public class BuildingNode : ActionNodeBase
    {
        public BuildingData SourceData;

        public override UIEventHandler UIEventHandler { get; protected set; }
        public override ActionType ActionType { get; }

        public float BuildingTime = 2;

        private float currentBuildingTime;

        private bool allItemsGathered;

        private ItemContainer Container;
        private InputNode inputNode;

        protected override void Awake()
        {
            base.Awake();
            Container = GetComponentInChildren<ItemContainer>();
            inputNode = GetComponentInChildren<InputNode>();
        }

        public void Initialize(BuildingData data)
        {
            SourceData = data;
            Container.Updated += OnContainerUpdated;
            inputNode.AddAcceptedItems(data.requiredItems);
        }

        public override bool TryGetAction(IEntity worker, out EntityAction action)
        {
            action = new EntityAction(this);
            return true;
        }

        public override IEnumerator UseCoroutine(ActionData data, CancellationTokenSource cancellationTokenSource)
        {
            while (currentBuildingTime < BuildingTime)
            {
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    yield break;
                }
                yield return 0;
                currentBuildingTime += Time.deltaTime;
            }
            CompleteBuilding();
        }

        private void CompleteBuilding()
        {
            
        }

        private void OnContainerUpdated()
        {
            // if (Container.HasItem())
            // {
            //     allItemsGathered = true;
            // }    
        }
    }
}