using Bunit;
using Corona.Components;
using Corona.Components.Models;
using Corona.Components.Navigation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace Corona.Tests;

public sealed class CoronaInteractiveLayoutTests : ComponentTestContext
{
    public CoronaInteractiveLayoutTests()
    {
        Services.AddCoronaLayout();
    }

    [Fact]
    public void HeaderIsland_MenuClick_TogglesSharedDrawerState()
    {
        var state = Services.GetRequiredService<CoronaDrawerStateService>();
        var cut = Render<CoronaHeaderIsland>(p => p.Add(x => x.Title, "Docs"));

        Assert.False(state.IsOpen);
        cut.Find(".corona-page-header__menu-button").Click();

        Assert.True(state.IsOpen);
    }

    [Fact]
    public void HeaderIsland_MenuClick_OpensInteractiveDrawer()
    {
        var drawer = Render<CoronaInteractiveDrawer>(p => p
            .Add(x => x.Title, "Nav")
            .Add(x => x.NavItems, Items()));
        var header = Render<CoronaHeaderIsland>(p => p.Add(x => x.Title, "Docs"));

        Assert.DoesNotContain("is-open", drawer.Find(".corona-drawer").ClassList);

        header.Find(".corona-page-header__menu-button").Click();

        drawer.WaitForAssertion(() =>
            Assert.Contains("is-open", drawer.Find(".corona-drawer").ClassList));
    }

    [Fact]
    public void InteractiveDrawer_OverlayClick_ClosesAndSyncsState()
    {
        var state = Services.GetRequiredService<CoronaDrawerStateService>();
        var cut = Render<CoronaInteractiveDrawer>(p => p.Add(x => x.Title, "Nav"));

        state.SetOpen(true);
        cut.WaitForAssertion(() =>
            Assert.Contains("is-open", cut.Find(".corona-drawer").ClassList));

        cut.Find(".corona-drawer-overlay").Click();

        Assert.False(state.IsOpen);
        Assert.DoesNotContain("is-open", cut.Find(".corona-drawer").ClassList);
    }

    [Fact]
    public void InteractiveDrawer_Navigation_ClosesDrawer()
    {
        var navigation = RegisterNavigation("http://localhost/");
        var state = Services.GetRequiredService<CoronaDrawerStateService>();
        var cut = Render<CoronaInteractiveDrawer>(p => p
            .Add(x => x.Title, "Nav")
            .Add(x => x.NavItems, Items()));

        state.SetOpen(true);
        cut.WaitForAssertion(() =>
            Assert.Contains("is-open", cut.Find(".corona-drawer").ClassList));

        navigation.RaiseLocationChanged();

        cut.WaitForAssertion(() =>
            Assert.DoesNotContain("is-open", cut.Find(".corona-drawer").ClassList));
        Assert.False(state.IsOpen);
    }

    [Fact]
    public void InteractiveDrawer_Navigation_StaysOpenWhenDisabled()
    {
        var navigation = RegisterNavigation("http://localhost/");
        var state = Services.GetRequiredService<CoronaDrawerStateService>();
        var cut = Render<CoronaInteractiveDrawer>(p => p
            .Add(x => x.Title, "Nav")
            .Add(x => x.CloseOnNavigation, false));

        state.SetOpen(true);
        cut.WaitForAssertion(() =>
            Assert.Contains("is-open", cut.Find(".corona-drawer").ClassList));

        navigation.RaiseLocationChanged();

        cut.WaitForAssertion(() =>
            Assert.Contains("is-open", cut.Find(".corona-drawer").ClassList));
    }

    [Fact]
    public void InteractiveDrawer_RendersTitleAndMenu_WhenNoChildContent()
    {
        var cut = Render<CoronaInteractiveDrawer>(p => p
            .Add(x => x.Title, "Navigation")
            .Add(x => x.NavItems, Items()));

        Assert.Equal("Navigation", cut.Find(".corona-interactive-drawer__title").TextContent);
        Assert.Equal(2, cut.FindAll(".corona-nav-menu__link").Count);
    }

    [Fact]
    public void InteractiveDrawer_ChildContentOverridesTitleAndMenu()
    {
        var cut = Render<CoronaInteractiveDrawer>(p => p
            .Add(x => x.Title, "Navigation")
            .Add(x => x.NavItems, Items())
            .AddChildContent("<strong>Custom content</strong>"));

        Assert.Empty(cut.FindAll(".corona-interactive-drawer__title"));
        Assert.Empty(cut.FindAll(".corona-nav-menu__link"));
        Assert.Contains("Custom content", cut.Markup);
    }

    [Fact]
    public void HeaderIsland_CustomMenuCallback_OverridesDrawerToggle()
    {
        var state = Services.GetRequiredService<CoronaDrawerStateService>();
        var clicked = false;
        var cut = Render<CoronaHeaderIsland>(p => p
            .Add(x => x.Title, "Docs")
            .Add(x => x.OnMenuClick, EventCallback.Factory.Create(this, () => clicked = true)));

        cut.Find(".corona-page-header__menu-button").Click();

        Assert.True(clicked);
        Assert.False(state.IsOpen);
    }

    private static IReadOnlyList<CoronaNavItem> Items() =>
    [
        new() { Text = "Home", Url = "/" },
        new() { Text = "About", Url = "/about" }
    ];

    private TestNavigationManager RegisterNavigation(string uri)
    {
        var navigation = new TestNavigationManager("http://localhost/", uri);
        Services.AddSingleton<NavigationManager>(navigation);
        return navigation;
    }

    private sealed class TestNavigationManager : NavigationManager
    {
        public TestNavigationManager(string baseUri, string uri)
        {
            Initialize(baseUri, uri);
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            Uri = ToAbsoluteUri(uri).ToString();
        }

        public void RaiseLocationChanged(bool isInterceptedLink = true) => NotifyLocationChanged(isInterceptedLink);
    }
}