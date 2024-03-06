using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Managers
{
    public class MonoBehaviourManager : MonoManager<MonoBehaviourManager>
    {
        public event Action MonoQuit;

        public bool quit;
        
        private readonly Queue<Action> actionsToCall = new Queue<Action>();
        private readonly List<IProcessable> processables = new List<IProcessable>();
        
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
        
        public void EnqueueCall(Action call) => actionsToCall.Enqueue(call);

        private void OnApplicationQuit()
        {
            quit = true;
            MonoQuit?.Invoke();
        }
    }
}