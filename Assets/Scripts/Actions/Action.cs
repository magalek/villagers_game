using Entities;
using UnityEngine;

namespace Actions
{
    public abstract class Action : IAction
    {
        public IActionProgress Progress => progress;

        protected readonly ActionProgress progress = new ActionProgress();
        
        protected ActionCancelationToken cancelationToken;

        public void Start(Entity entity)
        {
            Debug.Log($"Started new {GetType()}");
            OnStarted(entity);
        }

        protected virtual void OnStarted(Entity entity) { }
        
        public virtual void Cancel() => cancelationToken.Cancel();
    }
}