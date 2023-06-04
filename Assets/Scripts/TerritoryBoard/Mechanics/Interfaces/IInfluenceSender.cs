namespace TerritoryBoard.Mechanics
{
    public interface IInfluenceSender
    {
        public delegate void OnInfluenceSenderChanged(IInfluenceSender sender);
        public event OnInfluenceSenderChanged onInfluenceSenderChanged;
        public float TotalInfluenceOutput { get; }
        public float InfluenceOutput { get; }
        public float InfluenceOutputFactor { get; set; }
    }
}
