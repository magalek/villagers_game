using System.Collections;
using System.Collections.Generic;
using Entities;
using Interfaces;
using Managers;
using Movement;
using Targets;
using UnityEngine;

namespace Actions
{
    public class GatheringAction : MoveAction
    {
        private float currentGatheringTime;

        private IGatheringNode gatheringNode;

        public GatheringAction(IGatheringNode gatheringNode) 
            : base(gatheringNode)
        {
            this.gatheringNode = gatheringNode;
            this.gatheringNode.Changed += OnNodeChanged;
        }

        public override bool ShowProgress => true;

        private IEnumerator GatherCoroutine()
        {
            Debug.Log("started gather coroutine");
            cancelationToken = new ActionCancelationToken();
            progress.Reset();
            while (currentGatheringTime < gatheringNode.GatheringTime)
            {
                yield return NormalizeGatheringTime();
                if (cancelationToken!.canceled)
                {
                    cancelationToken = null;
                    yield break;
                }
                IncrementGatheringTime();
                progress.Update(NormalizeGatheringTime());
            }
            if (cancelationToken.canceled) yield break;
            
            ItemManager.Current.SpawnItemObject(
                gatheringNode.GatherItem(1), //TODO: change so harvest amount is influenced 
                gatheringNode.transform.position);
            Debug.Log("ended gather coroutine");

            cancelationToken = null;
        }

        private void IncrementGatheringTime()
        {
            currentGatheringTime = Mathf.Clamp(currentGatheringTime + Time.deltaTime, 0, gatheringNode.GatheringTime);
        }

        private float NormalizeGatheringTime()
        {
            return currentGatheringTime / gatheringNode.GatheringTime;
        }

        private void OnNodeChanged(GatheringNodeContext context)
        {
            if (context.gathered) Cancel();
        }

        protected override IEnumerator OnTargetReached(MoveDestination destination)
        {
            base.OnTargetReached(destination);
            return GatherCoroutine();
        }
    }
}