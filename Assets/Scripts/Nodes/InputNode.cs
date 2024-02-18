using System.Collections.Generic;
using Actions;
using Entities;
using Interfaces;
using Items;
using Map.Tiles;
using Targets;
using UI;
using UnityEngine;
using Utility;
using Action = System.Action;

namespace Nodes
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class InputNode : ActionNodeBase, IInputNode
    {
        [SerializeReference] private List<Item> acceptedItems = new List<Item>();
        public event Action Changed;
        public bool IsUsed { get; }
        public Vector2 Position => transform.position;
        public ActionType ActionType { get; }

        public ComponentGetter<IItemContainer> Container { get; private set; }
        public override UIEventHandler UIEventHandler { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            Container = new ComponentGetter<IItemContainer>(this);
            UIEventHandler = new ContainerUIEventHandler(Container.Get());
        }


        public bool CanBeUsedBy(IEntity worker)
        {
            return worker.ItemHolder.Get().HeldItem != null && acceptedItems.Contains(worker.ItemHolder.Get().HeldItem);
        }

        public IEnumerable<IAction> GetActions(IEntity worker)
        {
            return new List<IAction> {new HaulAction(worker, this)};
        }

        public void Add(IEnumerable<ItemEntry> entries)
        {
            foreach (var itemEntry in entries)
            {
                //Debug.Log(itemEntry);
                Container.Get().AddItem(itemEntry);
            }
        }

        public bool Accepts(IItem item)
        {
            return acceptedItems.Contains(item);
        }
    }
}