using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SupineSnail.DependencyInjection;

public class ServiceProvider : IServiceProvider
{
    private readonly Dictionary<Type, IInitializerInfo[]> _initializers;

    internal ServiceProvider(HashSet<IInitializerInfo> initializers)
    {
        _initializers = initializers
            .GroupBy(i => i.CreatedType)
            .ToDictionary(t => t.Key, t => t.ToArray());
    }

    public T? GetRequiredService<T>() where T : class
    {
        if (!TryGetService(out T? service))
            throw new InvalidOperationException(
                $"Service of '{typeof(T).FullName}' does not exist in the dependency injection container");

        return service;
    }

    public T? GetRequiredService<T>(string? name) where T : class
    {
        if (!TryGetService(name, out T? service))
            throw new InvalidOperationException(
                $"Service of '{typeof(T).FullName}' with name '{name}' does not exist in the dependency injection container");

        return service;
    }

    public T? GetService<T>() where T : class
    {
        return TryGetService(out T? service) 
            ? service 
            : default;
    }

    public T? GetService<T>(string? name) where T : class
    {
        return TryGetService(name, out T? service) 
            ? service 
            : default;
    }

    public object? GetService(Type type, string? name)
    {
        return TryGetService(type, name, out var service) 
            ? service 
            : default;
    }

    public bool TryGetService<T>(out T? service) where T : class
    {
        service = default;
            
        if (!TryGetService(typeof(T), out var serviceObj))
            return false;

        service = serviceObj as T;
        return true;
    }

    private bool TryGetService(Type type, out object? service)
    {
        service = default;
            
        if (!_initializers.ContainsKey(type))
            return false;

        service = _initializers[type].Last().GetInstance(this);
        return true;
    }

    public bool TryGetService<T>(string? name, out T? service) where T : class?
    {
        service = default;
            
        if (!TryGetService(typeof(T), name, out var serviceObj))
            return false;

        service = serviceObj as T;
        return true;
    }

    private bool TryGetService(Type type, string? name, out object? service)
    {
        service = default;
            
        if (!_initializers.ContainsKey(type))
            return false;

        var initializer = _initializers[type].LastOrDefault(i => i.TagName == name);
        if (initializer == null)
            return false;

        service = initializer.GetInstance(this);
        return true;
    }

    public IEnumerable<T> GetServices<T>() where T : class
    {
        return _initializers.ContainsKey(typeof(T)) 
            ? _initializers[typeof(T)].Select(i => i.GetInstance(this)).Cast<T>().ToArray() 
            : Array.Empty<T>();
    }

    public IEnumerable<T>? GetServices<T>(string? name) where T : class
        => GetServices(typeof(T), name) as IEnumerable<T>;

    public IEnumerable GetServices(Type type, string? name)
    {
        if (!_initializers.ContainsKey(type))
            return Array.Empty<object>();

        var initializers = _initializers[type].Where(i => i.TagName == name).ToArray();
        var values = initializers.Select(i => i.GetInstance(this)).ToArray();
        var listType = typeof(List<>).MakeGenericType(type);
            
        var list = Activator.CreateInstance(listType);

        var iList = (IList) list!;
        foreach (var value in values)
        {
            iList.Add(value);
        }

        return iList;
    }

    public void Dispose()
    {
        foreach (var initializer in _initializers.SelectMany(kvp => kvp.Value))
        {
            initializer.Dispose();
        }
            
        GC.SuppressFinalize(this);
    }
}