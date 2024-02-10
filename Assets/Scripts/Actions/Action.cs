using Entities;

namespace Actions
{
    public abstract class Action : IAction
    {
        public IActionProgress Progress => progress;

        protected readonly ActionProgress progress = new ActionProgress();
        
        public abstract void Start(Entity entity);
    }
}