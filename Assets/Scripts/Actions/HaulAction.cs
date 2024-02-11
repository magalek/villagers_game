using UnityEngine;

namespace Actions
{
    public class HaulAction : MoveAction
    {
        public HaulAction(Vector2 targetPosition) : base(targetPosition)
        {
            
        }

        protected override void OnMovementEnded()
        {
            
        }
    }
}