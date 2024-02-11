using System;
using Interfaces;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    public class ItemHolder : MonoBehaviour, IItemContainer
    {
        [SerializeField] private SpriteRenderer itemSprite;

        public IItem HeldItem => heldItem;
        
        private IItem heldItem;

        public event Action<IItem> ItemAdded;
        public event Action<IItem> ItemRemoved;

        public bool TryAddItem(IItem item)
        {
            if (heldItem != null) return false;
            heldItem = item;
            itemSprite.sprite = heldItem.Sprite;
            ItemAdded?.Invoke(HeldItem);
            return true;
        }

        public IItem TryRemoveItem(IItem item)
        {
            if (heldItem == null) return null;
            var heldCopy = heldItem.Copy();
            heldItem = null;
            itemSprite.sprite = null;
            ItemRemoved?.Invoke(heldCopy);
            return heldCopy;
        }

        public void DropItem(IItem item) => ManagerLoader.Get<ItemManager>().SpawnItemObject(item, transform.position);
        public void DropItem() => DropItem(RemoveHeld());

        public IItem RemoveHeld() => TryRemoveItem(heldItem);
    }
}