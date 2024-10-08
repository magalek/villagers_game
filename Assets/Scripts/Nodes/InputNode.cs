﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Actions;
using Entities;
using Interfaces;
using Items;
using Managers;
using UI;
using UnityEngine;

namespace Nodes
{

    public class InputNode : ActionNodeBase, IInputNode
    {
        [SerializeField] private List<Item> acceptedItems = new List<Item>();
        [SerializeField] private bool acceptsAll;

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
                if ((ActionNodeBase)node == this || node == null) continue;
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
            return null;
        }

        public void AddAcceptedItems(List<ContainerEntry> entries)
        {
            
        }

        public void AddAllItems() => acceptsAll = true;

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