public class BaseFactory<T> where T : new()
{
    public static BaseFactory<T> Instance { 
        get
        {
            if (_instance == null)
            {
                _instance = new BaseFactory<T>();
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    private static BaseFactory<T> _instance;

    private BaseFactory() { }

    public virtual T Create()
    {
        return default(T);
    }
}
