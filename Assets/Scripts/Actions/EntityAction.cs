using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using Interfaces;
using Managers;
using Nodes;
using UnityEngine;

namespace Actions
{
    public class EntityAction
    {
        public readonly Queue<ActionNodeBase> ActionNodes = new Queue<ActionNodeBase>();

        private ActionData Data;

        private CancellationTokenSource tokenSource;

        private ActionNodeBase currentNode;

        private EntityAction() => MonoBehaviourManager.Current.MonoQuit += () => tokenSource?.Cancel();

        public EntityAction(ActionNodeBase node) : this()
        {
            ActionNodes.Enqueue(node);
        }
        
        public EntityAction(ActionNodeBase node1, ActionNodeBase node2) : this()
        {
            ActionNodes.Enqueue(node1);
            ActionNodes.Enqueue(node2);
        }
        
        public EntityAction(IEnumerable<ActionNodeBase> nodes) : this()
        {
            foreach (var node in nodes)
            {
                ActionNodes.Enqueue(node);
            }
        }

        public IEnumerator WorkCoroutine(Entity entity, Action endCallback)
        {
            Data = new ActionData(entity);
            tokenSource = new CancellationTokenSource();
            while (ActionNodes.Count > 0 && !tokenSource.IsCancellationRequested)
            {
                currentNode = ActionNodes.Dequeue();
                currentNode.Lock(entity);
                if (currentNode) yield return entity.Movement.GoTo(currentNode.transform, tokenSource);
                if (currentNode) yield return currentNode.Use(Data, tokenSource);
                currentNode.Unlock();
            }
            if (!tokenSource.IsCancellationRequested) endCallback?.Invoke();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (Data != null) builder.AppendLine($"Entity: {Data.Entity}");
            if (currentNode != null)
            {
                builder.AppendLine("Current:");
                builder.AppendLine(currentNode.name);
            }
            builder.AppendLine("Queue");
            for (int i = 0; i < ActionNodes.Count; i++)
            {
                var node = ActionNodes.Dequeue();
                if (node) builder.AppendLine(node.name);
                ActionNodes.Enqueue(node);
            }
            return builder.ToString();
        }
    }
}