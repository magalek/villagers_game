using System.Linq;
using Interfaces;
using Items;
using Unity.Mathematics;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ItemManager : MonoManager<ItemManager>
    {
        [SerializeField] private ItemObject itemObjectPrefab;
        
        public void SpawnItemObject(IItem item, Vector2 position)
        {
            var itemPosition = position + (Vector2)Random.insideUnitSphere;
            Instantiate(itemObjectPrefab, MapManager.Current.Grid.GetAdjacentTiles(position).ToArray().RandomItem().transform.position, quaternion.identity).Initialize(item);
        }
    }
}