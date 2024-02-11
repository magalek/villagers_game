namespace Actions
{
    public class ActionCancelationToken
    {
        public bool shouldCancel;
        
        public void Cancel()
        {
            shouldCancel = true;
        }      
    }
}