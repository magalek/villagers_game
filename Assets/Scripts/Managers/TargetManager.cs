using Actions;
using Entities;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class TargetManager : IManager
    {
        public IActionTarget GetNearestTarget(Vector2 position, float radius)
        {
            const int actionLayer = 1 << 6;
            var results = Physics2D.OverlapCircleAll(position, radius, actionLayer);

            foreach (var result in results)
            {
                if (result.TryGetComponent(out IActionTarget target))
                {
                    if (target.IsUsed) continue;
                    return target;
                }
            }

            return null;
        }
    }
}