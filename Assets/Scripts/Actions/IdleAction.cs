using Entities;

namespace Actions
{
    public class IdleAction : Action
    {
        public override bool ShowProgress => false;

        public IdleAction()
        {
            progress.Complete();
        }
    }
}