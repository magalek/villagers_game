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

        private IGatheringTarget gatheringTarget;

        private readonly TargetManager targetManager = ManagerLoader.Get<TargetManager>();

        public GatheringAction(IGatheringTarget _gatheringTarget)
        {
            gatheringTarget = _gatheringTarget;
            gatheringTarget.Changed += OnTargetChanged;
        }
        
        protected override void OnStarted(Entity entity)
        {
            entity.StartCoroutine(GatherCoroutine(entity));
        }
        
        private IEnumerator GatherCoroutine(Entity entity)
        {
            cancelationToken = new ActionCancelationToken();
            while (currentGatheringTime < gatheringTarget.GatheringTime)
            {
                Debug.Log("in gathering task");
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
            var holder = entity.ItemHolder.Get();
            holder.TryAddItem(gatheringTarget.GatherItem());
            var inputTarget = targetManager.GetNearestInputForItem(entity, holder.HeldItem);
            if (inputTarget == null) holder.DropItem();
            else entity.ActionQueue.AddAction(new HaulAction(entity, inputTarget));
            cancelationToken = null;
        }

        private void IncrementGatheringTime()
        {
            currentGatheringTime = Mathf.Clamp(currentGatheringTime + Time.deltaTime, 0, gatheringTarget.GatheringTime);
        }

        private float NormalizeGatheringTime()
        {
            return currentGatheringTime / gatheringTarget.GatheringTime;
        }

        private void OnTargetChanged(GatheringTargetData data)
        {
            if (data.gathered) Cancel();
        }
    }
}