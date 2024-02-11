using System.Collections;
using System.Collections.Generic;
using Entities;
using Interfaces;
using UnityEngine;

namespace Actions
{
    public class GatheringAction : Action
    {
        private float currentGatheringTime;

        private IGatheringTarget gatheringTarget;

        public GatheringAction(IGatheringTarget _gatheringTarget)
        {
            gatheringTarget = _gatheringTarget;
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
                if (cancelationToken!.shouldCancel)
                {
                    yield return null;
                    cancelationToken = null;
                }
                IncrementGatheringTime();
                progress.Update(NormalizeGatheringTime());
            }
            gatheringTarget.Gather();
            entity.ActionQueue.AddAction(new HaulAction(Vector3.zero));
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
    }
}