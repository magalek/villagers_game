using Entities;

namespace Actions
{
    public class IdleAction : Action
    {
        public IdleAction()
        {
            progress.Update(1);
        }
        
        public override void Start(Entity entity)
        {
        }
    }
}