namespace Mechanics
{
    public interface IInfluenceSender
    {
        public delegate void OnChanged(IInfluenceSender sender);
        public event OnChanged onInfluenceSenderChanged;

        public float TotalInfluenceOutput { get; }
        public float InfluenceOutput { get; }
        public float InfluenceOutputFactor { get; set; }
    }
}
