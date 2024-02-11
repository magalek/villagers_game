using Actions;
using Interfaces;
using Movement;
using Utility;

namespace Entities
{
    public interface IEntity : IContainer
    {
        ActionQueue ActionQueue { get; }
        ComponentGetter<IMovement> Movement { get; }
    }
}