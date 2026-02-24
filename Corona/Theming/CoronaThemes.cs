namespace Corona.Theming;

/// <summary>
/// Provides built-in Corona themes.
/// </summary>
public static class CoronaThemes
{
    /// <summary>
    /// Creates the built-in light theme.
    /// </summary>
    public static CoronaTheme Light(CoronaThemeOverrides? overrides = null)
    {
        var primitive = CoronaPrimitiveTokens.Default;
        var baseTheme = new CoronaTheme(
            Name: "Corona Light",
            Mode: CoronaThemeMode.Light,
            Primitive: primitive,
            Semantic: CoronaSemanticTokens.CreateLight(primitive));

        return baseTheme.WithOverrides(overrides);
    }

    /// <summary>
    /// Creates the built-in dark theme.
    /// </summary>
    public static CoronaTheme Dark(CoronaThemeOverrides? overrides = null)
    {
        var primitive = CoronaPrimitiveTokens.Default;
        var baseTheme = new CoronaTheme(
            Name: "Corona Dark",
            Mode: CoronaThemeMode.Dark,
            Primitive: primitive,
            Semantic: CoronaSemanticTokens.CreateDark(primitive));

        return baseTheme.WithOverrides(overrides);
    }
}
