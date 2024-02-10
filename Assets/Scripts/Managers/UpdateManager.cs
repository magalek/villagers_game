using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Managers
{
    public class UpdateManager : IManager
    {
        private MonoBehaviourHost host;
        
        public void Call(Action call) => host.EnqueueCall(call);

        public void RegisterProcessable(IProcessable processable) => host.RegisterProcessable(processable);
        
        public UpdateManager()
        {
            host = new GameObject("Mono Behaviour Host").AddComponent<MonoBehaviourHost>();
        }
    }

    public class MonoBehaviourHost : MonoBehaviour
    {
        private readonly Queue<Action> actionsToCall = new Queue<Action>();
        private readonly List<IProcessable> processables = new List<IProcessable>();

        public void EnqueueCall(Action call)
        {
            actionsToCall.Enqueue(call);
        }

        public void RegisterProcessable(IProcessable processable) => processables.Add(processable);

        private void Update()
        {
            foreach (var processable in processables)
            {
                processable.Process();
            }
            
            while (actionsToCall.TryDequeue(out Action action))
            {
                action.Invoke();
            }
        }
    }
}