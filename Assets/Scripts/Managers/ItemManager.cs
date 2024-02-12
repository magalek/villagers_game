using Interfaces;
using Items;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class ItemManager : MonoManager
    {
        [SerializeField] private ItemObject itemObjectPrefab;
        
        public void SpawnItemObject(IItem item, Vector2 position)
        {
            Instantiate(itemObjectPrefab, position + (Vector2)Random.insideUnitSphere, quaternion.identity).Initialize(item);
        }
    }
}