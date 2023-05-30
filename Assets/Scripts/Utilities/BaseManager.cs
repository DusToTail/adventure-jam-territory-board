using System.Collections.Generic;

public class BaseManager<T> where T : IUniqueIdentifier, new()
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

    public Dictionary<int, T> Dictionary { get; private set; }
    public delegate void OnAdded(T x);
    public delegate void OnRemoved(T x);
    public event OnAdded onAdded;
    public event OnRemoved onRemoved;

    private static BaseManager<T> _instance;

    private BaseManager()
    {
        Dictionary = new Dictionary<int, T>();
    }

    public virtual void Add(T x)
    {
        if(x == null || Dictionary.ContainsKey(x.Id)) { return; }
        bool success = Dictionary.TryAdd(x.Id, x);
        
        if(success)
            onAdded?.Invoke(x);
    }
    public virtual void Remove(T x)
    {
        if (x == null || !Dictionary.ContainsKey(x.Id)) { return; }
        bool success = Dictionary.Remove(x.Id);

        if(success)
            onRemoved?.Invoke(x);
    }
    public virtual T Get(int id)
    {
        T result;
        bool success = Dictionary.TryGetValue(id, out result);
        return success ? result : default(T);
    }
}

public interface IUniqueIdentifier
{
    public int Id { get; }
}
