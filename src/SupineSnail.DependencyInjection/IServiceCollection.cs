using System;

namespace SupineSnail.DependencyInjection;

public interface IServiceCollection
{
    /// <summary>
    /// Adds <typeparamref name="T"/> as a singleton. Will use reflection to initialize constructor required types using the provider
    /// </summary>
    /// <typeparam name="T">Type to register as</typeparam>
    public void AddSingleton<T>() where T : class;
    /// <summary>
    /// Adds the instance as a singleton registered as <typeparamref name="T"/>, and does not lazy initialize
    /// </summary>
    /// <param name="instance">Instance to add</param>
    /// <typeparam name="T">Type to register as</typeparam>
    public void AddSingleton<T>(T instance) where T : class;
    /// <summary>
    /// Adds the instance as a named singleton registered as <typeparamref name="T"/>, and does not lazy initialize
    /// </summary>
    /// <param name="name">The name of the instance</param>
    /// <param name="instance">Instance to add</param>
    /// <typeparam name="T">Type to register as</typeparam>
    public void AddSingleton<T>(string? name, T instance) where T : class;
    /// <summary>
    /// Adds <typeparamref name="T"/> as a singleton, will use the initializer provided to create an instance
    /// </summary>
    /// <param name="initializer">Function to call to create an instance</param>
    /// <typeparam name="T">Type to register as</typeparam>
    public void AddSingleton<T>(Func<IServiceProvider, T> initializer) where T : class;
    /// <summary>
    /// Adds <typeparamref name="T"/> as a named singleton, will use the initializer provided to create an instance
    /// </summary>
    /// <param name="name">The name of the instance</param>
    /// <param name="initializer">Function to call to create an instance</param>
    /// <typeparam name="T">Type to register as</typeparam>
    public void AddSingleton<T>(string? name, Func<IServiceProvider, T> initializer) where T : class;
        
    /// <summary>
    /// Adds <typeparamref name="T"/> as a singleton registered as it's type <typeparamref name="TIFace"/>.
    /// Will use reflection to initialize constructor required types using the provider
    /// </summary>
    /// <typeparam name="TIFace">Type to register as</typeparam>
    /// <typeparam name="T">Type of the concrete instance</typeparam>
    public void AddSingleton<TIFace, T>() where T : class, TIFace where TIFace : class;
    /// <summary>
    /// Adds <typeparamref name="T"/> as a named singleton registered as it's type <typeparamref name="TIFace"/>.
    /// Will use reflection to initialize constructor required types using the provider
    /// </summary>
    /// <param name="name">The name of the instance</param>
    /// <typeparam name="TIFace">Type to register as</typeparam>
    /// <typeparam name="T">Type of the concrete instance</typeparam>
    public void AddSingleton<TIFace, T>(string? name) where T : class, TIFace where TIFace : class;
    /// <summary>
    /// Adds the instance as a singleton registered as <typeparamref name="TIFace"/>, and does not lazy initialize
    /// </summary>
    /// <typeparam name="TIFace">Type to register as</typeparam>
    /// <typeparam name="T">Type of the concrete instance</typeparam>
    public void AddSingleton<TIFace, T>(T instance) where T : class, TIFace where TIFace : class;
    /// <summary>
    /// Adds the instance passed in as a singleton as <typeparamref name="TIFace"/>, and does not lazy initialize
    /// </summary>
    /// <param name="name">The name of the instance</param>
    /// <param name="instance">Instance to add</param>
    /// <typeparam name="TIFace">Interface type to register as</typeparam>
    /// <typeparam name="T">Concrete type</typeparam>
    public void AddSingleton<TIFace, T>(string? name, T instance) where T : class, TIFace where TIFace : class;
    /// <summary>
    /// Adds <typeparamref name="T"/> as a singleton registered as <typeparamref name="TIFace"/>.
    /// Will use the initializer provided to create an instance
    /// </summary>
    /// <param name="initializer">Function to call to create an instance</param>
    /// <typeparam name="TIFace">Type to register as</typeparam>
    /// <typeparam name="T">Type of the concrete instance</typeparam>
    public void AddSingleton<TIFace, T>(Func<IServiceProvider, T> initializer) where T : class, TIFace where TIFace : class;
    /// <summary>
    /// Adds <typeparamref name="T"/> as a named singleton registered as <typeparamref name="TIFace"/>.
    /// Will use the initializer provided to create an instance
    /// </summary>
    /// <param name="name">The name of the instance</param>
    /// <param name="initializer">Function to call to create an instance</param>
    /// <typeparam name="TIFace">Type to register as</typeparam>
    /// <typeparam name="T">Type of the concrete instance</typeparam>
    public void AddSingleton<TIFace, T>(string? name, Func<IServiceProvider, T> initializer) where T : class, TIFace where TIFace : class;
        
    /// <summary>
    /// Builds the service provider
    /// </summary>
    /// <returns>The provider</returns>
    public IServiceProvider BuildProvider();
}