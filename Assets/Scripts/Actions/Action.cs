using Entities;
using UnityEngine;

namespace Actions
{
    public abstract class Action : IAction
    {
        public bool InProgress => (cancelationToken == null || !cancelationToken.canceled) && !progress.IsCompleted;
        public IActionProgress Progress => progress;
        public abstract bool ShowProgress { get; }

        protected readonly ActionProgress progress = new ActionProgress();
        
        protected ActionCancelationToken cancelationToken;

        public void Start(Entity entity)
        {
            Debug.Log($"Started new {GetType()}");
            OnStarted(entity);
        }

        protected virtual void OnStarted(Entity entity) { }
        
        public virtual void Cancel()
        {
            cancelationToken?.Cancel();
            progress.Complete();
        }
    }
}