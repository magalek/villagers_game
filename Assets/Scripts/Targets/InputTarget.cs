﻿using System.Collections.Generic;
using Actions;
using Entities;
using Interfaces;
using Items;
using Managers;
using Map.Tiles;
using UI;
using UnityEngine;
using Utility;
using Action = System.Action;

namespace Targets
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class InputTarget : ActionTargetBase, IInputTarget
    {
        [SerializeReference] private List<Item> acceptedItems = new List<Item>();

        public event Action Changed;
        public bool IsUsed { get; }
        public Vector2 Position => transform.position;
        public ActionType ActionType { get; }

        public ComponentGetter<IItemContainer> Container { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Container = new ComponentGetter<IItemContainer>(this);
        }

        public override void OnClick(MapTile tile)
        {
            ManagerLoader.Get<UIPanelInfoManager>().ShowPanelInfo(tile.GridPosition, Container.Get());
        }

        public bool CanUse(IEntity worker)
        {
            return worker.ItemHolder.Get().HeldItem != null && acceptedItems.Contains(worker.ItemHolder.Get().HeldItem);
        }

        public IEnumerable<IAction> GetActions(IEntity worker)
        {
            return new List<IAction> {new HaulAction(worker, this)};
        }

        public void Add(IItem item)
        {
            Debug.Log($"Added {item}");
            Container.Get().TryAddItem(item);
        }

        public bool Accepts(IItem item)
        {
            return acceptedItems.Contains(item);
        }
    }
}