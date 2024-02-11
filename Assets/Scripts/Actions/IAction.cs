using Entities;

namespace Actions
{
    public interface IAction
    {
        bool InProgress { get; }
        IActionProgress Progress { get; }
        void Start(Entity entity);
        void Cancel();
    }
}