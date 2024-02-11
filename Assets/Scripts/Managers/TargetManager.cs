using Actions;
using Entities;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class TargetManager : IManager
    {
        public IActionTarget GetNearestTarget(IEntity entity)
        {
            var results = GetNearestTargets(entity);

            foreach (var result in results)
            {
                if (result.TryGetComponent(out IActionTarget actionTarget))
                {
                    if (actionTarget.IsUsed) continue;
                    return actionTarget;
                }
            }
            return null;
        }

        public IInputTarget GetNearestInputForItem(IEntity entity, IItem item)
        {
            var results = GetNearestTargets(entity);
            
            foreach (var result in results)
            {
                if (result.TryGetComponent(out IInputTarget input))
                {
                    if (!input.Accepts(item)) continue;
                    return input;
                }
            }
            return null;
        }

        private Collider2D[] GetNearestTargets(IEntity entity)
        {
            const int actionLayer = 1 << 6;
            return Physics2D.OverlapCircleAll(entity.Movement.Get().Position, entity.Statistics.actionSearchRadius, actionLayer);
        }
    }
}