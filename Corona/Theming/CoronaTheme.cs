namespace Corona.Theming;

/// <summary>
/// Defines available built-in color modes.
/// </summary>
public enum CoronaThemeMode
{
    Light,
    Dark
}

/// <summary>
/// Aggregates primitive and semantic tokens into a full theme.
/// </summary>
public sealed record CoronaTheme(
    string Name,
    CoronaThemeMode Mode,
    CoronaPrimitiveTokens Primitive,
    CoronaSemanticTokens Semantic)
{
    /// <summary>
    /// Creates a new theme by applying optional overrides to this instance.
    /// </summary>
    public CoronaTheme WithOverrides(CoronaThemeOverrides? overrides)
    {
        if (overrides is null)
        {
            return this;
        }

        var primitive = overrides.Primitive is null
            ? Primitive
            : Primitive with
            {
                Colors = overrides.Primitive.Colors ?? Primitive.Colors,
                Spacing = overrides.Primitive.Spacing ?? Primitive.Spacing,
                Radius = overrides.Primitive.Radius ?? Primitive.Radius,
                Shadows = overrides.Primitive.Shadows ?? Primitive.Shadows,
                Typography = overrides.Primitive.Typography ?? Primitive.Typography
            };

        var semanticFromPrimitive = Mode == CoronaThemeMode.Dark
            ? CoronaSemanticTokens.CreateDark(primitive)
            : CoronaSemanticTokens.CreateLight(primitive);

        var semantic = overrides.Semantic is null
            ? semanticFromPrimitive
            : semanticFromPrimitive with
            {
                ColorPrimary = overrides.Semantic.ColorPrimary ?? semanticFromPrimitive.ColorPrimary,
                SurfaceBackground = overrides.Semantic.SurfaceBackground ?? semanticFromPrimitive.SurfaceBackground,
                SurfaceBackgroundAlt = overrides.Semantic.SurfaceBackgroundAlt ?? semanticFromPrimitive.SurfaceBackgroundAlt,
                CardBackground = overrides.Semantic.CardBackground ?? semanticFromPrimitive.CardBackground,
                TextPrimary = overrides.Semantic.TextPrimary ?? semanticFromPrimitive.TextPrimary,
                TextSecondary = overrides.Semantic.TextSecondary ?? semanticFromPrimitive.TextSecondary,
                BorderDefault = overrides.Semantic.BorderDefault ?? semanticFromPrimitive.BorderDefault,
                FocusOutline = overrides.Semantic.FocusOutline ?? semanticFromPrimitive.FocusOutline,
                ElevationCard = overrides.Semantic.ElevationCard ?? semanticFromPrimitive.ElevationCard,
                RadiusCard = overrides.Semantic.RadiusCard ?? semanticFromPrimitive.RadiusCard,
                SpacingCard = overrides.Semantic.SpacingCard ?? semanticFromPrimitive.SpacingCard,
                SpacingCardHeader = overrides.Semantic.SpacingCardHeader ?? semanticFromPrimitive.SpacingCardHeader,
                FontFamilyDefault = overrides.Semantic.FontFamilyDefault ?? semanticFromPrimitive.FontFamilyDefault,
                FontSizeBody = overrides.Semantic.FontSizeBody ?? semanticFromPrimitive.FontSizeBody,
                FontSizeHeading = overrides.Semantic.FontSizeHeading ?? semanticFromPrimitive.FontSizeHeading,
                FontWeightHeading = overrides.Semantic.FontWeightHeading ?? semanticFromPrimitive.FontWeightHeading
            };

        return this with { Primitive = primitive, Semantic = semantic };
    }
}

/// <summary>
/// Contains optional primitive token overrides.
/// </summary>
public sealed record CoronaPrimitiveTokenOverrides(
    CoronaColorPrimitives? Colors = null,
    CoronaSpacingPrimitives? Spacing = null,
    CoronaRadiusPrimitives? Radius = null,
    CoronaShadowPrimitives? Shadows = null,
    CoronaTypographyPrimitives? Typography = null);

/// <summary>
/// Contains optional semantic token overrides.
/// </summary>
public sealed record CoronaSemanticTokenOverrides(
    string? ColorPrimary = null,
    string? SurfaceBackground = null,
    string? SurfaceBackgroundAlt = null,
    string? CardBackground = null,
    string? TextPrimary = null,
    string? TextSecondary = null,
    string? BorderDefault = null,
    string? FocusOutline = null,
    string? ElevationCard = null,
    string? RadiusCard = null,
    string? SpacingCard = null,
    string? SpacingCardHeader = null,
    string? FontFamilyDefault = null,
    string? FontSizeBody = null,
    string? FontSizeHeading = null,
    string? FontWeightHeading = null);

/// <summary>
/// Represents partial theme overrides that can be applied per app or per component.
/// </summary>
public sealed record CoronaThemeOverrides(
    CoronaPrimitiveTokenOverrides? Primitive = null,
    CoronaSemanticTokenOverrides? Semantic = null);
