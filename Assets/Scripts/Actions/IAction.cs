namespace Entities
{
    public interface IAction
    {
        IActionProgress Progress { get; }
        void Start(Entity entity);
    }
}