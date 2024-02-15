using System.Collections;
using System.Collections.Generic;
using Entities;
using Interfaces;
using Managers;
using Targets;
using UnityEngine;

namespace Actions
{
    public class GatheringAction : Action
    {
        private float currentGatheringTime;

        private IGatheringNode gatheringNode;

        public GatheringAction(IGatheringNode gatheringNode)
        {
            this.gatheringNode = gatheringNode;
            this.gatheringNode.Changed += OnNodeChanged;
        }

        public override bool ShowProgress => true;

        protected override void OnStarted(Entity entity)
        {
            entity.StartCoroutine(GatherCoroutine(entity));
        }
        
        private IEnumerator GatherCoroutine(Entity entity)
        {
            cancelationToken = new ActionCancelationToken();
            while (currentGatheringTime < gatheringNode.GatheringTime)
            {
                //Debug.Log("in gathering task");
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
            GatherItem(entity);
            cancelationToken = null;
        }

        private void GatherItem(Entity entity)
        {
            var holder = entity.ItemHolder.Get();
            holder.TryAddItem(gatheringNode.GatherItem());
            var inputTarget = NodeHelper.GetNearestInputNode(entity, holder.HeldItem);
            if (inputTarget == null) holder.DropItem();
            else entity.ActionQueue.AddActions(inputTarget.GetActions(entity));
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
    }
}