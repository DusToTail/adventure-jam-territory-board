namespace Mechanics
{
    public interface IInfluenceSender
    {
        public float Total { get; }
        public float Amount { get; }
        public float Factor { get; set; }
    }
}
