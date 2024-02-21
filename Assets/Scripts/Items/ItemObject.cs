using System.Collections.Generic;
using Actions;
using Entities;
using Interfaces;
using Targets;
using UI;
using UnityEngine;

namespace Items
{
    public class ItemObject : ActionNodeBase, IOutputNode
    {
        [SerializeField] private SpriteRenderer itemRenderer;

        public ItemEntry itemEntry;

        public void Initialize(ItemEntry _itemEntry)
        {
            itemEntry = _itemEntry;
            itemRenderer.sprite = itemEntry.item.Sprite;
        }

        public override UIEventHandler UIEventHandler { get; protected set; }

        public override ActionType ActionType { get; }
        public override bool TryGetActions(IEntity worker, out List<IAction> actions)
        {
            actions = new List<IAction>();
            return false;
        }

        public override void OnReachedTarget(IEntity entity)
        {
            Debug.Log($"reached {name}");
            entity.ItemHolder.Get().AddItem(itemEntry);
            Destroy(gameObject);
        }

        public bool ContainsItem(ItemEntry _itemEntry) => _itemEntry.Equals(itemEntry);
        public bool ContainsItem(Item item) => itemEntry.item.Equals(item);

        public ItemEntry GetItem()
        {
            Destroy(gameObject);
            return itemEntry;
        }
    }
}