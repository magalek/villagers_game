using Items;
using UnityEngine;

namespace Managers
{
    public class ItemManager : MonoManager<ItemManager>
    {
        [SerializeField] private ItemObject itemObjectPrefab;

        public void SpawnItemObject(Item item, int amount, Vector2 position)
        {
            var tile = MapManager.Current.Grid.FindFirstEmpty(position);
            var itemObject = Instantiate(itemObjectPrefab);
            itemObject.Initialize(item, amount);
            tile.AddObject(itemObject);
        }
    }
}