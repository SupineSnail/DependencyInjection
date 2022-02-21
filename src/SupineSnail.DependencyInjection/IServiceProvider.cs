using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace SupineSnail.DependencyInjection;

public interface IServiceProvider : IDisposable
{
    T? GetRequiredService<T>() where T : class;
    T? GetRequiredService<T>(string? name) where T : class;

    T? GetService<T>() where T : class;
    T? GetService<T>(string? name) where T : class;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    object? GetService(Type type, string? name);

    bool TryGetService<T>(out T? service) where T : class;
    bool TryGetService<T>(string? name, out T? service) where T : class;

    IEnumerable<T> GetServices<T>() where T : class;
    IEnumerable<T>? GetServices<T>(string? name) where T : class;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    IEnumerable? GetServices(Type type, string? name);
}