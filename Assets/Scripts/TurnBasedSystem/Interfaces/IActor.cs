namespace TurnBasedSystem
{
    public interface IActor : Utilities.IUniqueIdentifier
    {
        public bool HasSentInput { get; set; }
        public bool CanSendInput { get; set; }
        public bool HasFinishedAction { get; set; }
        public void ResetState();
        public IAction SubmitAction();
    }
}
