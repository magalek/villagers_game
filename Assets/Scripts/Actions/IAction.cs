using Entities;

namespace Actions
{
    public interface IAction
    {
        bool InProgress { get; }
        IActionProgress Progress { get; }
        bool ShowProgress { get; }
        void Start(Entity entity);
        void Cancel();
    }
}