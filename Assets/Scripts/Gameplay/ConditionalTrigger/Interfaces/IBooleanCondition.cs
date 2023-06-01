namespace Gameplay
{
    public interface IBooleanCondition
    {
        public bool Value { get; }
        public delegate bool OnConditionChanged(IBooleanCondition condition);
        public event OnConditionChanged onConditionChanged;
    }
}
