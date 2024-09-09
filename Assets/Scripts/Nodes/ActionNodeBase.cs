using System;
using System.Collections;
using System.Threading;
using Actions;
using Entities;
using Interfaces;
using Targets;
using UI;
using UnityEngine;

namespace Nodes
{
    public abstract class ActionNodeBase : BaseSceneMonoBehaviour, IActionNode, IUIEventTarget
    {
        public event Action<IPlaceable> WillDestroy;

        private const int NODE_TARGET_LAYER = 6;

        protected IEntity lockerEntity;
        
        protected override void Awake()
        {
            base.Awake();
            gameObject.layer = NODE_TARGET_LAYER;
        }

        public Coroutine Use(ActionData data, CancellationTokenSource cancellationTokenSource) =>
            StartCoroutine(UseCoroutine(data, cancellationTokenSource));
        
        public abstract UIEventHandler UIEventHandler { get; protected set; }
        public abstract ActionType ActionType { get; }
        public abstract bool TryGetAction(IEntity worker, out EntityAction action);
        public abstract IEnumerator UseCoroutine(ActionData data, CancellationTokenSource cancellationTokenSource);
        public void Lock(IEntity entity) => lockerEntity = entity;
        public void Unlock() => lockerEntity = null;
        protected void DestroyNode()
        {
            WillDestroy?.Invoke(this);
            Destroy(gameObject);
        }
    }
}