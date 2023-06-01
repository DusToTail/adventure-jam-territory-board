using System;

namespace Gameplay
{
    public abstract class TriggerAction : ITrigger
    {
        public Action Action { get; set; }

        public void OnTrigger()
        {
            Action.Invoke();
        }
    }
    public abstract class TriggerFunc<T> : ITrigger<T>
    {
        public Func<T> Func { get; set; }

        public T OnTrigger()
        {
            return Func.Invoke();
        }
    }
}
