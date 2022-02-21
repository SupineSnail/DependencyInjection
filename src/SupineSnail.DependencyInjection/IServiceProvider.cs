using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace SupineSnail.DependencyInjection;

/// <summary>
/// A DI Container modeled on Microsoft's DI. Only supports basic singleton implementation
/// </summary>
public interface IServiceProvider : IDisposable
{
    /// <summary>
    /// Gets a service from the provider. Throws an exception if <typeparamref name="T"/> is not found.
    /// </summary>
    /// <typeparam name="T">Type of service to retrieve</typeparam>
    /// <exception cref="InvalidOperationException">If the service was not found</exception>
    /// <returns>The service instance</returns>
    T GetRequiredService<T>() where T : class;
    
    /// <summary>
    /// Gets a named service from the provider. Throws an exception if <typeparamref name="T"/> is not found.
    /// </summary>
    /// <param name="name">A name for the instance you want</param>
    /// <typeparam name="T">Type of service to retrieve</typeparam>
    /// <exception cref="InvalidOperationException">If the service was not found</exception>
    /// <returns>The service instance</returns>
    T GetRequiredService<T>(string? name) where T : class;

    /// <summary>
    /// Gets a service from the provider. Returns null if <typeparamref name="T"/> is not found.
    /// </summary>
    /// <typeparam name="T">Type of service to retrieve</typeparam>
    /// <returns>The service instance</returns>
    T? GetService<T>() where T : class;
    
    /// <summary>
    /// Gets a named service from the provider. Returns null if <typeparamref name="T"/> is not found.
    /// </summary>
    /// <param name="name">A name for the instance you want</param>
    /// <typeparam name="T">Type of service to retrieve</typeparam>
    /// <returns>The service instance</returns>
    T? GetService<T>(string? name) where T : class;

    /// <summary>
    /// Gets a service from the provider for the type specified
    /// </summary>
    /// <param name="type">The type to get a service for</param>
    /// <param name="name">A name for the instance you want. Can be null</param>
    /// <returns>The service instance as an object</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    object? GetService(Type type, string? name);

    /// <summary>
    /// Tries to get a service from the provider.
    /// </summary>
    /// <param name="service">The service found</param>
    /// <typeparam name="T">Type of service to retrieve</typeparam>
    /// <returns>True if service is found</returns>
    bool TryGetService<T>(out T? service) where T : class;

    /// <summary>
    /// Tries to get a named service from the provider.
    /// </summary>
    /// <param name="name">The name of the service</param>
    /// <param name="service">The service found</param>
    /// <typeparam name="T">Type of service to retrieve</typeparam>
    /// <returns>True if service is found</returns>
    bool TryGetService<T>(string? name, out T? service) where T : class;

    /// <summary>
    /// Gets all the services from the provider matching <typeparamref name="T"/> and returns them as an IEnumerable&lt;<typeparamref name="T"/>&gt;.
    /// </summary>
    /// <typeparam name="T">Type of service to retrieve</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all instances of the type</returns>
    IEnumerable<T> GetServices<T>() where T : class;
    
    /// <summary>
    /// Gets all the named services from the provider matching <typeparamref name="T"/> and returns them as an IEnumerable&lt;<typeparamref name="T"/>&gt;.
    /// </summary>
    /// <param name="name">A name for the instance you want</param>
    /// <typeparam name="T">Type of service to retrieve</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all instances of the type</returns>
    IEnumerable<T>? GetServices<T>(string? name) where T : class;

    /// <summary>
    /// Gets all the named services from the provider matching the passed in type and returns them as an IEnumerable&lt;<typeparamref name="T"/>&gt;.
    /// </summary>
    /// <param name="type">Type of service to retrieve</param>
    /// <param name="name">A name for the instance you want. Can be null</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all instances of the type</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IEnumerable? GetServices(Type type, string? name);
}