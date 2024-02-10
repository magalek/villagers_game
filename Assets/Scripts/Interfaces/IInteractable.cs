using Entities;
using UnityEngine;

namespace Interfaces
{
    public interface IInteractable<in TEntity> where TEntity : IEntity
    {
        bool CanInteract(TEntity entity);
        void Interact(TEntity entity);
    }
}