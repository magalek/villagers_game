using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Actions;
using Entities;
using Interfaces;
using Nodes;
using UI;
using UnityEngine;

namespace Items
{
    public class ItemObject : ActionNodeBase, IOutputNode
    {
        [SerializeField] private SpriteRenderer itemRenderer;
        
        public override UIEventHandler UIEventHandler { get; protected set; }

        public override ActionType ActionType { get; }

        public Item item;
        public int amount;

        public void Initialize(Item _item, int _amount)
        {
            item = _item;
            amount = _amount;
            itemRenderer.sprite = item.Sprite;
        }

        public override bool TryGetAction(IEntity worker, out EntityAction action)
        {
            action = null;
            return false;
        }

        public override IEnumerator UseCoroutine(ActionData data, CancellationTokenSource cancellationTokenSource)
        {
            data.Item = item;
            DestroyNode();
            yield break;
        }
        
        public bool ContainsItem(Item _item) => item.Equals(_item);
    }
}