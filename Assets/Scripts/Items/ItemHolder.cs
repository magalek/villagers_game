using System;
using System.Collections.Generic;
using Interfaces;
using Managers;
using UnityEngine;

namespace Items
{
    public class ItemHolder : MonoBehaviour, IItemContainer
    {
        [SerializeField] private SpriteRenderer itemSprite;

        public Item HeldItem => heldItem;
        
        private Item heldItem;

        public event Action Updated;
        public event Action<Item> ItemAdded;
        public event Action<Item> ItemRemoved;

        public IReadOnlyList<ItemEntry> Items => new[] { new ItemEntry(HeldItem, 1) };

        public void AddItem(ItemEntry itemEntry)
        {
            heldItem = itemEntry.item;
            itemSprite.sprite = heldItem.Sprite;
            ItemAdded?.Invoke(HeldItem);
            Updated?.Invoke();
        }

        public void RemoveItem(ItemEntry itemEntry) => RemoveItem();

        public IEnumerable<ItemEntry> RemoveAll()
        {
            yield return new ItemEntry(heldItem, 1);
            RemoveItem();
        }

        private void RemoveItem()
        {
            itemSprite.sprite = null;
            ItemRemoved?.Invoke(heldItem);
            heldItem = null;
            Updated?.Invoke();
        }

        public bool HasItem(Item item) => item.Id == heldItem.Id;

        public int Count(Item item) => heldItem == item ? 1 : 0;

        public void DropItem() => ItemManager.Current.SpawnItemObject(heldItem, transform.position);
        
        public bool HasItem(IItem item) => heldItem.IsEqual(item);
    }
}