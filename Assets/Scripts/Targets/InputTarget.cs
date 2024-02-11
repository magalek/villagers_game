using System.Collections.Generic;
using Actions;
using Entities;
using Interfaces;
using Items;
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

        public bool TryGetActions(IEntity worker, out Queue<IAction> actions)
        {
            actions = null;
            return false;
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