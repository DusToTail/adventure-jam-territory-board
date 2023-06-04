using System.Linq;

namespace TerritoryBoard.TurnBasedSystem
{
    public class PerActorTurnController : TurnController
    {
        private ITurnBasedActor _currentActor;
        private int _currentActorIndex;

        public PerActorTurnController()
        {
            _currentActorIndex = 0;
        }

        public override void BeginTurn()
        {
            var turn = new PerActorTurn(Turns.Count.ToString());
            _currentActor = _actorManager.Dictionary.Values.ToArray()[_currentActorIndex];
            _currentActor.ResetState();
            turn.Actor = _currentActor;
            _currentActorIndex++;
            _currentActorIndex %= _actorManager.Dictionary.Keys.Count;
            Turns.Add(turn);
        }
    }
}