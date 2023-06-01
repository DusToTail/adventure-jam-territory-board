using System.Threading.Tasks;

namespace TerritoryBoard.TurnBasedSystem
{
    public class PerActorTurn : Turn
    {
        public IActor Actor
        {
            get
            {
                if (Actors.Count == 0)
                {
                    return null;
                }
                return Actors[0];
            }
            set
            {
                if(Actors.Count == 0)
                {
                    Actors.Add(value);
                }
                Actors[0] = value;
            }
        }
        public IAction Action
        {
            get
            {
                if(Actions.Count == 0)
                {
                    return null;
                }
                return Actions[0];
            }
            set
            {
                if (Actions.Count == 0)
                {
                    Actions.Add(value);
                }
                Actions[0] = value;
            }
        }

        public PerActorTurn(string id) : base(id)
        {
        }

        public override bool ReadyForExcution()
        {
            return Actor.HasSentInput;
        }
        public override bool HasFinishedExecution()
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