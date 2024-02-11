using Entities;
using Interfaces;
using UnityEngine;

namespace Actions
{
    public class HaulAction : MoveAction
    {
        private IEntity entity;
        private IInputTarget inputTarget;
        
        public HaulAction(IEntity _entity, IInputTarget _inputTarget) : base(_inputTarget.Position)
        {
            entity = _entity;
            inputTarget = _inputTarget;
        }

        protected override void OnMovementEnded()
        {
            var item = entity.ItemHolder.Get().RemoveHeld();
            inputTarget.Add(item);
        }
    }
}