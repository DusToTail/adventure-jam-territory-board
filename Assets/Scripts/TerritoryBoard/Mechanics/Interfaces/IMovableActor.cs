namespace TerritoryBoard.Mechanics
{
    public interface IMovableActor : IMechanicsActor
    {
        public int currentXCoordinate { get;}
        public int currentYCoordinate { get;}
        public void MoveToPosition(int x, int y);
    }
}
