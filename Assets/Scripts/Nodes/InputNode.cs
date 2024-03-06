using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Actions;
using Entities;
using Interfaces;
using Items;
using Managers;
using Targets;
using UI;
using UnityEngine;
using Utility;

namespace Nodes
{

    public class InputNode : ActionNodeBase, IInputNode
    {
        [SerializeReference] private List<Item> acceptedItems = new List<Item>();
        public event Action Changed;
        public bool IsUsed { get; }

        public override ActionType ActionType { get; }

        public IItemContainer Container { get; private set; }
        public override UIEventHandler UIEventHandler { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            Container = GetComponentInChildren<IItemContainer>();
            UIEventHandler = new ContainerUIEventHandler(Container);
        }

        public override bool TryGetAction(IEntity worker, out EntityAction action)
        {
            action = null;
            foreach (var activeTile in MapManager.Current.Grid.GetSortedActiveTilesByDistance(transform.position))
            {
                var node = activeTile.GetActionNode<IOutputNode>();
                if (node == null) continue;
                if (acceptedItems.Any(item => node.ContainsItem(item)))
                {
                    action = new EntityAction(node as ActionNodeBase, this);
                    return true;
                }
            }
            return false;
        }

        public override IEnumerator UseCoroutine(ActionData data, CancellationTokenSource cancellationTokenSource)
        {
            Add(data.Item);
            data.Item = null;
            yield break;
        }

        public void Add(Item item)
        {
            Container.AddItem(new ContainerEntry(item, 1));
        }

        public bool Accepts(Item item)
        {
            return acceptedItems.Contains(item);
        }
    }
}