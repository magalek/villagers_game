using Actions;
using Interfaces;
using Items;
using UnityEngine;
using Utility;

namespace Entities
{
    public interface IEntity 
    {
        Transform transform { get; }
        GameObject gameObject { get; }
        EntityAction CurrentAction { get; }
        EntityStatistics Statistics { get; }
        EntityMovement Movement { get; }    
        IItemContainer Container { get; }
        ItemHolder ItemHolder { get; }
    }
}