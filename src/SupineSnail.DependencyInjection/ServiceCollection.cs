using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SupineSnail.DependencyInjection;

public class ServiceCollection : IServiceCollection
{
    private readonly HashSet<IInitializerInfo> _initializers = new();

    public void AddSingleton<T>() where T : class
        => AddSingleton<T, T>();

    public void AddSingleton<T>(T instance) where T : class
        => AddSingleton<T, T>(instance);

    public void AddSingleton<T>(string? name, T instance) where T : class
        => AddSingleton<T, T>(name, instance);

    public void AddSingleton<T>(Func<IServiceProvider, T> initializer) where T : class
        => AddSingleton<T, T>(initializer);

    public void AddSingleton<T>(string? name, Func<IServiceProvider, T> initializer) where T : class
        => AddSingleton<T, T>(name, initializer);

    public void AddSingleton<TIFace, T>() where T : class, TIFace where TIFace : class
        => AddSingleton<TIFace, T>((string?) null);

    public void AddSingleton<TIFace, T>(string? name) where T : class, TIFace where TIFace : class
    {
        var type = typeof(T);
        var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
        if (!constructors.Any())
            throw new InvalidOperationException("Passed in type to register must have an instance constructor");

        var ctor = constructors
            .Select(c => (info: c, parameters: c.GetParameters()))
            .OrderBy(c => c.parameters.Length)
            .First();

        _initializers.Add(new InitializerInfo<TIFace>(name, provider => GetDeclaredInstance<TIFace>(name, provider, ctor)));
    }

    public void AddSingleton<TIFace, T>(T instance) where T : class, TIFace where TIFace : class
    {
        _initializers.Add(new InitializerInfo<TIFace>(null, _ => instance));
    }

    public void AddSingleton<TIFace, T>(string? name, T instance) where T : class, TIFace where TIFace : class
    {
        _initializers.Add(new InitializerInfo<TIFace>(name, _ => instance));
    }

    public void AddSingleton<TIFace, T>(Func<IServiceProvider, T> initializer) where T : class, TIFace where TIFace : class
    {
        _initializers.Add(new InitializerInfo<TIFace>(null, initializer));
    }

    public void AddSingleton<TIFace, T>(string? name, Func<IServiceProvider, T> initializer) where T : class, TIFace where TIFace : class
    {
        _initializers.Add(new InitializerInfo<TIFace>(name, initializer));
    }

    public IServiceProvider BuildProvider()
    {
        return new ServiceProvider(_initializers);
    }

    private T GetDeclaredInstance<T>(string? name, IServiceProvider provider,
        (ConstructorInfo info, ParameterInfo[] parameters) ctor) where T : class
    {
        var argValues = ctor.parameters.Select(p => GetService(name, provider, p)).ToArray();
        return (T) ctor.info.Invoke(argValues);
    }

    private object? GetService(string? name, IServiceProvider provider, ParameterInfo parameter)
    {
        var type = parameter.ParameterType;
        if (!type.IsGenericType) 
            return provider.GetService(type, name);
        
        var definition = type.GetGenericTypeDefinition();
        if (definition != typeof(IEnumerable<>))
            return provider.GetService(type, name);
        
        var typeParam = parameter.ParameterType.GenericTypeArguments.First();
        return provider.GetServices(typeParam, name);

    }
}