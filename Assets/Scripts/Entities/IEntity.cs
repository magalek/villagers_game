using Actions;
using Interfaces;
using Items;
using Movement;
using Utility;

namespace Entities
{
    public interface IEntity 
    {
        ActionQueue ActionQueue { get; }
        EntityStatistics Statistics { get; }
        ComponentGetter<IMovement> Movement { get; }    
        ComponentGetter<IItemContainer> Container { get; }
        ComponentGetter<ItemHolder> ItemHolder { get; }
    }
}