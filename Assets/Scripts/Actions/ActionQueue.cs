using System;
using System.Collections.Generic;
using Entities;
using Managers;
using UnityEngine;

namespace Actions
{
    public class ActionQueue
    {
        private readonly Queue<IAction> Actions = new Queue<IAction>();

        public IAction currentAction = new IdleAction();

        public void AddAction(IAction action) => Actions.Enqueue(action);

        public void AddActions(IEnumerable<IAction> actions)
        {
            foreach (var action in actions) AddAction(action);
        }

        public bool GetNextAction(out IAction action) => Actions.TryDequeue(out action);
    }
}