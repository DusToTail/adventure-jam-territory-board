using System.Linq;

namespace TerritoryBoard.TurnBasedSystem
{
    public class PerCycleTurnController : TurnController
    {
        private IActor[] _currentActors;

        public PerCycleTurnController()
        {
        }
        public override void BeginTurn()
        {
            var turn = new PerCycleTurn(Turns.Count.ToString());
            _currentActors = _actorManager.Dictionary.Values.ToArray();
            foreach (var actor in _currentActors)
            {
                actor.ResetState();
                turn.AddActor(actor);
            }
            Turns.Add(turn);
        }
    }
}
