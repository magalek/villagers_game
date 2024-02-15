using Actions;
using Entities;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public static class NodeHelper 
    {
        public static IActionNode GetNearestNode(IEntity entity)
        {
            var results = GetNearestNodes(entity);

            foreach (var result in results)
            {
                if (result.TryGetComponent(out IActionNode node))
                {
                    if (!node.CanBeUsedBy(entity)) continue;
                    return node;
                }
            }
            return null;
        }

        public static IInputNode GetNearestInputNode(IEntity entity, IItem item)
        {
            var results = GetNearestNodes(entity);
            
            foreach (var result in results)
            {
                if (result.TryGetComponent(out IInputNode node))
                {
                    if (!node.Accepts(item)) continue;
                    return node;
                }
            }
            return null;
        }

        private static Collider2D[] GetNearestNodes(IEntity entity)
        {
            const int actionLayer = 1 << 6;
            return Physics2D.OverlapCircleAll(entity.Movement.Get().Position, entity.Statistics.actionSearchRadius, actionLayer);
        }
    }
}