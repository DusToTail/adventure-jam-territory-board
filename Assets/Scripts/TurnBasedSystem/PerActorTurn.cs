using System.Threading.Tasks;

namespace TurnBasedSystem
{
    public class PerActorTurn : Turn
    {
        public IActor Actor { get; set; }
        public IAction Action { get; set; }


        public PerActorTurn(string id) : base(id)
        {
        }

        public override bool ReadyForExcution()
        {
            return Actor.HasSentInput;
        }
        public override bool FinishedExcution()
        {
            return Actor.HasFinishedAction;
        }
        public override Task Execute()
        {
            return Task.Run(() =>
            {
                Action.Execute();
            });
        }
        public override void AddActor(IActor actor)
        {
            Actor = actor;
        }
        public override void AddAction(IAction action)
        {
            Action = action;
        }
    }
}