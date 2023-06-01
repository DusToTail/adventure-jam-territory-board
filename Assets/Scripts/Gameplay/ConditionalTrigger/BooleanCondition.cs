namespace Gameplay
{
    public abstract class BooleanCondition : IBooleanCondition
    {
        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                bool invoke = _value != value;
                _value = value;
                if(invoke)
                {
                    onConditionChanged?.Invoke(this);
                }
            }
        }
        public event IBooleanCondition.OnConditionChanged onConditionChanged;
        protected bool _value = false;
    }
}
