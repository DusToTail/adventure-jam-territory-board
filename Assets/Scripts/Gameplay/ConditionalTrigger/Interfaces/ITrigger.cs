namespace Gameplay
{
    public interface ITrigger<T>
    {
        public T OnTrigger();
    }
    public interface ITrigger
    {
        public void OnTrigger();
    }
}
