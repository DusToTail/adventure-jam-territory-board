using System.Collections.Generic;

namespace TerritoryBoard.Utilities
{
    public class BaseManager<T> where T : IUniqueIdentifier
    {
        public static BaseManager<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BaseManager<T>();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public Dictionary<string, T> Dictionary { get; private set; }
        public delegate void OnAdded(T x);
        public delegate void OnRemoved(T x);
        public event OnAdded onAdded;
        public event OnRemoved onRemoved;

        private static BaseManager<T> _instance;

        protected BaseManager()
        {
            Dictionary = new Dictionary<string, T>();
        }

        public virtual void Add(T x)
        {
            if (x == null || Dictionary.ContainsKey(x.Id)) { return; }
            bool success = Dictionary.TryAdd(x.Id, x);

            if (success)
                onAdded?.Invoke(x);
        }
        public virtual void Remove(T x)
        {
            if (x == null || !Dictionary.ContainsKey(x.Id)) { return; }
            bool success = Dictionary.Remove(x.Id);

            if (success)
                onRemoved?.Invoke(x);
        }
        public virtual T Get(string id)
        {
            T result;
            bool success = Dictionary.TryGetValue(id, out result);
            return success ? result : default(T);
        }
    }

    public interface IUniqueIdentifier
    {
        public string Id { get; }
    }
}
