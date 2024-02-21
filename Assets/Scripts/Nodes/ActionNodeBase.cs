using System;
using System.Collections.Generic;
using Actions;
using Entities;
using Interfaces;
using Map.Tiles;
using UI;
using UnityEngine;
using Action = System.Action;

namespace Targets
{
    public abstract class ActionNodeBase : BaseSceneMonoBehaviour, IActionNode, IUIEventTarget
    {
        public event Action<IPlaceable> Destroyed;

        private const int NODE_TARGET_LAYER = 6;
        
        protected override void Awake()
        {
            base.Awake();
            gameObject.layer = NODE_TARGET_LAYER;
        }
        
        public abstract UIEventHandler UIEventHandler { get; protected set; }
        public abstract ActionType ActionType { get; }
        public abstract bool TryGetActions(IEntity worker, out List<IAction> actions);

        public abstract void OnReachedTarget(IEntity entity);
        
        protected void OnDestroyed() => Destroyed?.Invoke(this);
        
    }
}