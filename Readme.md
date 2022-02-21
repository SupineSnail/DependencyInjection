# SupineSnail Dalamud DependencyInjection

Adds basic dependency injection for singleton-only references (With interface definitions and JIT creation) and overridden disposal to avoid the .NET GC from calling at the wrong time. For Dalamud plugins.