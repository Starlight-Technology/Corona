using Microsoft.Extensions.DependencyInjection;

namespace Corona.Theming;

/// <summary>
/// Runtime theme state service for Corona components.
/// </summary>
public sealed class CoronaThemeProvider
{

    /// <summary>
    /// Occurs when the current theme changes.
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Initializes a new provider with an optional starting theme.
    /// </summary>
    public CoronaThemeProvider(CoronaTheme? initialTheme = null)
    {
        CurrentTheme = initialTheme ?? CoronaThemes.Light();
    }

    /// <summary>
    /// Gets the active runtime theme.
    /// </summary>
    public CoronaTheme CurrentTheme { get; private set; }

    /// <summary>
    /// Applies a complete theme and notifies subscribers.
    /// </summary>
    public void SetTheme(CoronaTheme theme)
    {
        CurrentTheme = theme;
        OnChange?.Invoke();
    }

    /// <summary>
    /// Switches between built-in light and dark themes.
    /// </summary>
    public void ToggleTheme(CoronaThemeOverrides? overrides = null)
    {
        var nextTheme = CurrentTheme.Mode == CoronaThemeMode.Light
            ? CoronaThemes.Dark(overrides)
            : CoronaThemes.Light(overrides);

        SetTheme(nextTheme);
    }
}

/// <summary>
/// Dependency injection extensions for Corona theming.
/// </summary>
public static class CoronaThemeServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Corona runtime theming provider.
    /// </summary>
    public static IServiceCollection AddCoronaTheming(
        this IServiceCollection services,
        CoronaTheme? initialTheme = null)
    {
        services.AddSingleton(_ => new CoronaThemeProvider(initialTheme));
        return services;
    }
}
