using System;
using Entities;
using Interfaces;
using UnityEngine;

namespace Buildings
{
    public class Building : MonoBehaviour, IPlaceable
    {
        public event Action<IPlaceable> WillDestroy;
        public void OnReachedTarget(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}