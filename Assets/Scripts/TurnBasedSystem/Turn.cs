using System.Threading.Tasks;

namespace TurnBasedSystem
{
    public abstract class Turn : Utilities.IUniqueIdentifier
    {
        public string Id { get { return _id; }}
        private string _id;

        public Turn(string id)
        {
            _id = id;
        }

        public abstract bool ReadyForExcution();
        public abstract bool FinishedExcution();
        public abstract Task Execute();
        public abstract void AddActor(IActor actor);
        public abstract void AddAction(IAction action);
    }
}
