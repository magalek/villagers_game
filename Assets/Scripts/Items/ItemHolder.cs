using System;
using System.Collections.Generic;
using Interfaces;
using Managers;
using UnityEngine;

namespace Items
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer itemSprite;

        public Item HeldItem { get; private set; }

        public event Action Updated;
        public event Action<Item> ItemAdded;
        public event Action<Item> ItemRemoved;

        public void SetItem(Item item)
        {
            HeldItem = item;
            itemSprite.sprite = HeldItem.Sprite;
            ItemAdded?.Invoke(HeldItem);
            Updated?.Invoke();
        }

        public void ClearItem()
        {
            itemSprite.sprite = null;
            ItemRemoved?.Invoke(HeldItem);
            HeldItem = null;
            Updated?.Invoke();
        }
    }
}