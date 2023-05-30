namespace Mechanics
{
    public interface IMovableActor : IActor
    {
        public int currentXCoordinate { get;}
        public int currentYCoordinate { get;}
        public void MoveToPosition(int x, int y);
    }
}
