namespace Tests.CSharp.Homework3;

public class SingleInitializationSingleton
{
    public const int DefaultDelay = 3_000;
    private static readonly object Locker = new();

    private static Lazy<SingleInitializationSingleton>? _singleton = new
        Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(100), true);

    private static volatile bool _isInitialized = false;

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;

        // imitation of complex initialization logic
        Thread.Sleep(delay);
    }

    public int Delay { get; }

    public static SingleInitializationSingleton Instance => _singleton.Value;

    internal static void Reset()
    {
        if (_isInitialized)
        {
            lock (Locker)
            {
                if (_isInitialized)
                {
                    _singleton = null;
                    _isInitialized = false;
                }
            }
        }
    }

    public static void Initialize(int delay)
    {
        if (_isInitialized)
            throw new InvalidOperationException("Object already initialized!");
        
        if (!_isInitialized)
        {
            lock (Locker)
            {
                if (!_isInitialized)
                {
                    _singleton = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(delay), true);
                    _isInitialized = true;
                }
            }
        }
    }
}