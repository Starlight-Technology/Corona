namespace Corona.Theming;

/// <summary>
/// Defines semantic design tokens consumed by components.
/// Semantic tokens always map to intent-based meanings.
/// </summary>
public sealed record CoronaSemanticTokens(
    string ColorPrimary,
    string SurfaceBackground,
    string SurfaceBackgroundAlt,
    string CardBackground,
    string TextPrimary,
    string TextSecondary,
    string BorderDefault,
    string FocusOutline,
    string ElevationCard,
    string RadiusCard,
    string SpacingCard,
    string SpacingCardHeader,
    string FontFamilyDefault,
    string FontSizeBody,
    string FontSizeHeading,
    string FontWeightHeading)
{
    /// <summary>
    /// Creates semantic tokens from primitive tokens for a light palette.
    /// </summary>
    public static CoronaSemanticTokens CreateLight(CoronaPrimitiveTokens primitives)
    {
        return new(
        ColorPrimary: primitives.Colors.Brand600,
        SurfaceBackground: primitives.Colors.Gray50,
        SurfaceBackgroundAlt: primitives.Colors.White,
        CardBackground: primitives.Colors.White,
        TextPrimary: primitives.Colors.Gray900,
        TextSecondary: primitives.Colors.Gray700,
        BorderDefault: primitives.Colors.Gray200,
        FocusOutline: primitives.Colors.Brand200,
        ElevationCard: primitives.Shadows.Md,
        RadiusCard: primitives.Radius.Lg,
        SpacingCard: primitives.Spacing.Md,
        SpacingCardHeader: primitives.Spacing.Md,
        FontFamilyDefault: primitives.Typography.FontFamily,
        FontSizeBody: primitives.Typography.SizeMd,
        FontSizeHeading: primitives.Typography.SizeLg,
        FontWeightHeading: primitives.Typography.WeightSemibold);
    }

    /// <summary>
    /// Creates semantic tokens from primitive tokens for a dark palette.
    /// </summary>
    public static CoronaSemanticTokens CreateDark(CoronaPrimitiveTokens primitives)
    {
        return new(
        ColorPrimary: primitives.Colors.Brand500,
        SurfaceBackground: primitives.Colors.Gray900,
        SurfaceBackgroundAlt: primitives.Colors.Gray700,
        CardBackground: primitives.Colors.Gray700,
        TextPrimary: primitives.Colors.Gray50,
        TextSecondary: primitives.Colors.Gray100,
        BorderDefault: primitives.Colors.Gray400,
        FocusOutline: primitives.Colors.Brand200,
        ElevationCard: primitives.Shadows.Lg,
        RadiusCard: primitives.Radius.Lg,
        SpacingCard: primitives.Spacing.Md,
        SpacingCardHeader: primitives.Spacing.Md,
        FontFamilyDefault: primitives.Typography.FontFamily,
        FontSizeBody: primitives.Typography.SizeMd,
        FontSizeHeading: primitives.Typography.SizeLg,
        FontWeightHeading: primitives.Typography.WeightSemibold);
    }
}
