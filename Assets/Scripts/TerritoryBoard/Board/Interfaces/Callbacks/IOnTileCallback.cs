namespace TerritoryBoard
{
    public interface IOnTileCallback : IOnTileEnter, IOnTileExit
    {
    }
    public interface IOnTileEnter
    {
        public void OnTileEnter(ITile tile);
    }
    public interface IOnTileExit
    {
        public void OnTileExit(ITile tile);
    }
}
