namespace Corona.Theming;

/// <summary>
/// Defines the raw, low-level primitive design tokens used to build semantic tokens.
/// </summary>
public sealed record CoronaPrimitiveTokens(
    CoronaColorPrimitives Colors,
    CoronaSpacingPrimitives Spacing,
    CoronaRadiusPrimitives Radius,
    CoronaShadowPrimitives Shadows,
    CoronaTypographyPrimitives Typography)
{
    /// <summary>
    /// Gets the default primitive token set.
    /// </summary>
    public static CoronaPrimitiveTokens Default { get; } = new(
        new CoronaColorPrimitives(),
        new CoronaSpacingPrimitives(),
        new CoronaRadiusPrimitives(),
        new CoronaShadowPrimitives(),
        new CoronaTypographyPrimitives());
}

/// <summary>
/// Raw color primitives.
/// </summary>
public sealed record CoronaColorPrimitives(
    string White = "#FFFFFF",
    string Black = "#000000",
    string Gray50 = "#F8FAFC",
    string Gray100 = "#F1F5F9",
    string Gray200 = "#E2E8F0",
    string Gray400 = "#94A3B8",
    string Gray700 = "#334155",
    string Gray900 = "#0F172A",
    string Brand500 = "#2563EB",
    string Brand600 = "#1D4ED8",
    string Brand200 = "#BFDBFE");

/// <summary>
/// Raw spacing primitives.
/// </summary>
public sealed record CoronaSpacingPrimitives(
    string Xs = "0.25rem",
    string Sm = "0.5rem",
    string Md = "1rem",
    string Lg = "1.5rem",
    string Xl = "2rem");

/// <summary>
/// Raw border radius primitives.
/// </summary>
public sealed record CoronaRadiusPrimitives(
    string Sm = "0.375rem",
    string Md = "0.75rem",
    string Lg = "1rem");

/// <summary>
/// Raw shadow primitives.
/// </summary>
public sealed record CoronaShadowPrimitives(
    string Sm = "0 1px 2px rgba(15, 23, 42, 0.10)",
    string Md = "0 8px 18px rgba(15, 23, 42, 0.12)",
    string Lg = "0 14px 30px rgba(15, 23, 42, 0.16)");

/// <summary>
/// Raw typography primitives.
/// </summary>
public sealed record CoronaTypographyPrimitives(
    string FontFamily = "Inter, system-ui, -apple-system, Segoe UI, Roboto, Helvetica, Arial, sans-serif",
    string SizeSm = "0.875rem",
    string SizeMd = "1rem",
    string SizeLg = "1.125rem",
    string WeightMedium = "500",
    string WeightSemibold = "600");
