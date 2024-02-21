using Actions;
using Interfaces;
using Items;
using Movement;
using UnityEngine;
using Utility;

namespace Entities
{
    public interface IEntity 
    {
        Transform transform { get; }
        GameObject gameObject { get; }
        ActionQueue ActionQueue { get; }
        EntityStatistics Statistics { get; }
        ComponentGetter<IMovement> Movement { get; }    
        ComponentGetter<IItemContainer> Container { get; }
        ComponentGetter<ItemHolder> ItemHolder { get; }
    }
}