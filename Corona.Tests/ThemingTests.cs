using Corona.Theming;
using Microsoft.Extensions.DependencyInjection;

namespace Corona.Tests;

public sealed class ThemingTests
{
    [Fact]
    public void LightTheme_HasExpectedDefaults()
    {
        var theme = CoronaThemes.Light();

        Assert.Equal(CoronaThemeMode.Light, theme.Mode);
        Assert.Equal(theme.Primitive.Colors.Brand600, theme.Semantic.ColorPrimary);
    }

    [Fact]
    public void DarkTheme_HasExpectedDefaults()
    {
        var theme = CoronaThemes.Dark();

        Assert.Equal(CoronaThemeMode.Dark, theme.Mode);
        Assert.Equal(theme.Primitive.Colors.Brand500, theme.Semantic.ColorPrimary);
    }

    [Fact]
    public void WithOverrides_UpdatesPrimitiveAndSemantic()
    {
        var baseTheme = CoronaThemes.Light();
        var overrides = new CoronaThemeOverrides(
            Primitive: new CoronaPrimitiveTokenOverrides(
                Colors: new CoronaColorPrimitives(Brand600: "#111111"),
                Typography: new CoronaTypographyPrimitives(FontFamily: "Test Font")),
            Semantic: new CoronaSemanticTokenOverrides(
                SurfaceBackground: "#222222",
                FontSizeBody: "13px"));

        var theme = baseTheme.WithOverrides(overrides);

        Assert.Equal("#111111", theme.Primitive.Colors.Brand600);
        Assert.Equal("#222222", theme.Semantic.SurfaceBackground);
        Assert.Equal("13px", theme.Semantic.FontSizeBody);
        Assert.Equal("Test Font", theme.Semantic.FontFamilyDefault);
    }

    [Fact]
    public void ThemeProvider_ToggleTheme_SwitchesModesAndRaisesEvent()
    {
        var provider = new CoronaThemeProvider(CoronaThemes.Light());
        var eventCount = 0;
        provider.OnChange += () => eventCount++;

        provider.ToggleTheme();
        provider.ToggleTheme();

        Assert.Equal(CoronaThemeMode.Light, provider.CurrentTheme.Mode);
        Assert.Equal(2, eventCount);
    }

    [Fact]
    public void ServiceCollection_AddCoronaTheming_RegistersProvider()
    {
        var services = new ServiceCollection();
        var initial = CoronaThemes.Dark();

        services.AddCoronaTheming(initial);

        using var sp = services.BuildServiceProvider();
        var provider = sp.GetRequiredService<CoronaThemeProvider>();

        Assert.Equal(CoronaThemeMode.Dark, provider.CurrentTheme.Mode);
    }
}
