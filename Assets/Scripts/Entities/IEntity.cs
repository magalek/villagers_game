using Movement;
using Utility;

namespace Entities
{
    public interface IEntity
    {
        ComponentGetter<IMovement> Movement { get; }
    }
}