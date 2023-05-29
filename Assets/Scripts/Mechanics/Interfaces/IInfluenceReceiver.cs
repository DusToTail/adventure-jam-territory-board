namespace Mechanics
{
    public interface IInfluenceReceiver
    {
        public float Total { get; }
        public float Amount { get; }
        public float Factor { get; set; }
    }
}
