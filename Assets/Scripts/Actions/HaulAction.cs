using Entities;
using Interfaces;
using UnityEngine;

namespace Actions
{
    public class HaulAction : MoveAction
    {
        private IEntity entity;
        private IInputNode inputNode;
        
        public HaulAction(IEntity _entity, IInputNode inputNode) : base(inputNode.Position)
        {
            entity = _entity;
            this.inputNode = inputNode;
        }

        protected override void OnMovementEnded()
        {
            var item = entity.ItemHolder.Get().RemoveAll();
            inputNode.Add(item);
        }
    }
}