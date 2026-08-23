using Microsoft.Extensions.DependencyInjection;

namespace Corona.Components.Navigation;

/// <summary>
/// Shared runtime state that keeps interactive header and drawer in sync.
/// </summary>
public sealed class CoronaDrawerStateService
{
    /// <summary>
    /// Occurs when the drawer open state changes.
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Gets whether the drawer is currently open.
    /// </summary>
    public bool IsOpen { get; private set; }

    /// <summary>
    /// Toggles the drawer open state and notifies subscribers.
    /// </summary>
    public void Toggle()
    {
        IsOpen = !IsOpen;
        OnChange?.Invoke();
    }

    /// <summary>
    /// Sets the drawer open state and notifies subscribers.
    /// </summary>
    public void SetOpen(bool open)
    {
        IsOpen = open;
        OnChange?.Invoke();
    }
}

/// <summary>
/// Dependency injection extensions for Corona layout components.
/// </summary>
public static class CoronaLayoutServiceCollectionExtensions
{
    /// <summary>
    /// Registers shared services used by Corona interactive layout components.
    /// </summary>
    public static IServiceCollection AddCoronaLayout(this IServiceCollection services)
    {
        services.AddScoped<CoronaDrawerStateService>();
        return services;
    }
}