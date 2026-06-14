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

        var primitive = ApplyPrimitiveOverrides(overrides.Primitive);
        var semantic = CreateSemanticTokens(primitive, overrides.Semantic);

        return this with { Primitive = primitive, Semantic = semantic };
    }

    private CoronaPrimitiveTokens ApplyPrimitiveOverrides(
        CoronaPrimitiveTokenOverrides? overrides)
    {
        return overrides is null
            ? Primitive
            : Primitive with
            {
                Colors = overrides.Colors ?? Primitive.Colors,
                Spacing = overrides.Spacing ?? Primitive.Spacing,
                Radius = overrides.Radius ?? Primitive.Radius,
                Shadows = overrides.Shadows ?? Primitive.Shadows,
                Typography = overrides.Typography ?? Primitive.Typography
            };
    }

    private CoronaSemanticTokens CreateSemanticTokens(
        CoronaPrimitiveTokens primitive,
        CoronaSemanticTokenOverrides? overrides)
    {
        var semantic = Mode == CoronaThemeMode.Dark
            ? CoronaSemanticTokens.CreateDark(primitive)
            : CoronaSemanticTokens.CreateLight(primitive);

        if (overrides is null)
        {
            return semantic;
        }

        semantic = ApplySemanticColorOverrides(semantic, overrides);
        return ApplySemanticLayoutOverrides(semantic, overrides);
    }

    private static CoronaSemanticTokens ApplySemanticColorOverrides(
        CoronaSemanticTokens semantic,
        CoronaSemanticTokenOverrides overrides)
    {
        return semantic with
        {
            ColorPrimary = overrides.ColorPrimary ?? semantic.ColorPrimary,
            SurfaceBackground = overrides.SurfaceBackground ?? semantic.SurfaceBackground,
            SurfaceBackgroundAlt = overrides.SurfaceBackgroundAlt ?? semantic.SurfaceBackgroundAlt,
            CardBackground = overrides.CardBackground ?? semantic.CardBackground,
            TextPrimary = overrides.TextPrimary ?? semantic.TextPrimary,
            TextSecondary = overrides.TextSecondary ?? semantic.TextSecondary,
            BorderDefault = overrides.BorderDefault ?? semantic.BorderDefault,
            FocusOutline = overrides.FocusOutline ?? semantic.FocusOutline
        };
    }

    private static CoronaSemanticTokens ApplySemanticLayoutOverrides(
        CoronaSemanticTokens semantic,
        CoronaSemanticTokenOverrides overrides)
    {
        return semantic with
        {
            ElevationCard = overrides.ElevationCard ?? semantic.ElevationCard,
            RadiusCard = overrides.RadiusCard ?? semantic.RadiusCard,
            SpacingCard = overrides.SpacingCard ?? semantic.SpacingCard,
            SpacingCardHeader = overrides.SpacingCardHeader ?? semantic.SpacingCardHeader,
            FontFamilyDefault = overrides.FontFamilyDefault ?? semantic.FontFamilyDefault,
            FontSizeBody = overrides.FontSizeBody ?? semantic.FontSizeBody,
            FontSizeHeading = overrides.FontSizeHeading ?? semantic.FontSizeHeading,
            FontWeightHeading = overrides.FontWeightHeading ?? semantic.FontWeightHeading
        };
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
