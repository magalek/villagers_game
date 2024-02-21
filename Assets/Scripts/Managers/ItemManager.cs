using Items;
using UnityEngine;

namespace Managers
{
    public class ItemManager : MonoManager<ItemManager>
    {
        [SerializeField] private ItemObject itemObjectPrefab;

        public void SpawnItemObject(ItemEntry itemEntry, Vector2 position)
        {
            var tile = MapManager.Current.Grid.FindFirstEmpty(position);
            var item = Instantiate(itemObjectPrefab);
            item.Initialize(itemEntry);
            tile.AddObject(item);
        }
    }
}