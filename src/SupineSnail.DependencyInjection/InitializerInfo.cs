using System;

namespace SupineSnail.DependencyInjection;

internal interface IInitializerInfo : IDisposable
{
    Type CreatedType { get; }
    string? TagName { get; }
    object? GetInstance(IServiceProvider provider);
}

internal abstract class InitializerInfoBase<T> : IInitializerInfo
{
    internal InitializerInfoBase(string? tagName)
    {
        CreatedType = typeof(T);
        TagName = tagName;
    }

    public Type CreatedType { get; }
    public string? TagName { get; }
        
    object? IInitializerInfo.GetInstance(IServiceProvider provider)
    {
        return GetInstance(provider);
    }

    protected abstract T? GetInstance(IServiceProvider provider);
    protected abstract void Dispose();

    void IDisposable.Dispose()
    {
        Dispose();
        GC.SuppressFinalize(this);
    }
}
    
internal class InitializerInfo<T> : InitializerInfoBase<T>
{
    private readonly Func<IServiceProvider,T> _initializer;
    private readonly object _initializeLock = new();
    private bool _isInitialized;
    private T? _instance;

    internal InitializerInfo(string? name, Func<IServiceProvider, T> initializer) : base(name)
    {
        _initializer = initializer;
    }

    protected override T? GetInstance(IServiceProvider provider)
    {
        if (_isInitialized)
            return _instance;
        
        lock (_initializeLock)
        {
            if (_isInitialized)
                return _instance;
        
            _instance = Initialize(provider);
            return _instance;
        }
    }

    protected override void Dispose()
    {
        if (_isInitialized && _instance is IPluginDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    private T Initialize(IServiceProvider provider)
    {
        _instance = _initializer(provider);
        _isInitialized = true;

        return _instance;
    }
}