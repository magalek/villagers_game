using Actions;
using Entities;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public static class NodeHelper 
    {
        // public static IActionNode GetNearestNode(IEntity entity)
        // {
        //     foreach (var tile in MapManager.Current.Grid.GetSortedTilesByDistance(entity.transform.position))
        //     {
        //         foreach (var node in tile.Nodes)
        //         {
        //             if (!node.CanBeUsedBy(entity)) continue;
        //             return node;
        //         }
        //     }
        //     return null;
        // }

        private static Collider2D[] GetNearestNodes(IEntity entity)
        {
            const int actionLayer = 1 << 6;
            return Physics2D.OverlapCircleAll(entity.Movement.Get().Position, entity.Statistics.actionSearchRadius, actionLayer);
        }
    }
}