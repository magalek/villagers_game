namespace Actions
{
    public class ActionCancelationToken
    {
        public bool canceled;
        
        public void Cancel()
        {
            canceled = true;
        }      
    }
}