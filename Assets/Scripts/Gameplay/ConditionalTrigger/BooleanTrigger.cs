namespace Gameplay
{
    public class BooleanTriggerAction : ITrigger
    {
        private IBooleanCondition _condition;
        private ITrigger _trigger;

        public BooleanTriggerAction(IBooleanCondition condition, ITrigger trigger)
        {
            _condition = condition;
            _trigger = trigger;
            _condition.onConditionChanged += (x) =>
            {
                OnTrigger();
                return true;
            };
        }

        public virtual void OnTrigger()
        {
            _trigger.OnTrigger();
        }
    }

    public class BooleanTriggerFunc : ITrigger<bool>
    {
        private IBooleanCondition _condition;
        private ITrigger<bool> _trigger;

        public BooleanTriggerFunc(IBooleanCondition condition, ITrigger<bool> trigger)
        {
            _condition = condition;
            _trigger = trigger;
            _condition.onConditionChanged += (x) =>
            {
                return OnTrigger();
            };
        }

        public virtual bool OnTrigger()
        {
            return _trigger.OnTrigger();
        }
    }
}
