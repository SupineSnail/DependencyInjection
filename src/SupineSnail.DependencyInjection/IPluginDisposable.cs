using System;

namespace SupineSnail.DependencyInjection;

/// <summary>
/// A specific instance of IDisposable, because if we dispose Dalamud stuff in the container that will cause chaos
/// </summary>
public interface IPluginDisposable : IDisposable
{
        
}