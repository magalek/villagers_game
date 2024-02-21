using System.Collections.Generic;
using System.Linq;
using Actions;
using Entities;
using Interfaces;
using Items;
using Managers;
using Targets;
using UI;
using UnityEngine;
using Utility;
using Action = System.Action;

namespace Nodes
{
    public class InputNode : ActionNodeBase, IInputNode
    {
        [SerializeReference] private List<Item> acceptedItems = new List<Item>();
        public event Action Changed;
        public bool IsUsed { get; }

        public override ActionType ActionType { get; }

        public ComponentGetter<IItemContainer> Container { get; private set; }
        public override UIEventHandler UIEventHandler { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            Container = new ComponentGetter<IItemContainer>(this);
            UIEventHandler = new ContainerUIEventHandler(Container.Get());
        }

        public override bool TryGetActions(IEntity worker, out List<IAction> actions)
        {
            actions = new List<IAction>();
            foreach (var activeTile in MapManager.Current.Grid.GetSortedActiveTilesByDistance(transform.position))
            {
                var node = activeTile.GetNode<IOutputNode>();
                if (node == null) continue;
                if (acceptedItems.Any(item => node.ContainsItem(item)))
                {
                    actions.Add(new HaulAction(node, this));
                    return true;
                }
            }
            return false;
        }

        public override void OnReachedTarget(IEntity entity)
        {
            Debug.Log($"reached {name}");
            Add(entity.ItemHolder.Get().RemoveAll());
        }

        public void Add(IEnumerable<ItemEntry> entries)
        {
            foreach (var itemEntry in entries)
            {
                //Debug.Log(itemEntry);
                Container.Get().AddItem(itemEntry);
            }
        }

        public bool Accepts(Item item)
        {
            return acceptedItems.Contains(item);
        }
    }
}