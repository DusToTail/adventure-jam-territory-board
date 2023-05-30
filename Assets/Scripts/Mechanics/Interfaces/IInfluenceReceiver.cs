namespace Mechanics
{
    public interface IInfluenceReceiver
    {
        public float TotalInfluenceInput { get; }
        public float InfluenceInput { get; }
        public float InfluenceInputFactor { get; set; }
    }
}
