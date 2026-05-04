using Bunit;
using Corona.Components;
using Corona.Components.Enums;
using Corona.Components.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Corona.Tests;

public sealed class ComponentRenderingTests : ComponentTestContext
{
    [Fact]
    public void CoronaButton_Click_RunsCallback_WhenEnabled()
    {
        var clicked = false;
        var cut = RenderComponent<CoronaButton>(p => p
            .AddChildContent("Save")
            .Add(x => x.OnClick, EventCallback.Factory.Create(this, () => clicked = true)));

        cut.Find("button").Click();

        Assert.True(clicked);
    }

    [Fact]
    public void CoronaButton_Loading_ShowsSpinnerAndDisablesButton()
    {
        var clicked = false;
        var cut = RenderComponent<CoronaButton>(p => p
            .Add(x => x.Loading, true)
            .Add(x => x.OnClick, EventCallback.Factory.Create(this, () => clicked = true))
            .AddChildContent("Ignored"));

        cut.Find("button").Click();

        Assert.NotNull(cut.Find(".corona-button__spinner"));
        Assert.False(clicked);
    }

    [Fact]
    public void CoronaBadge_RendersDotAndPositionClass()
    {
        var cut = RenderComponent<CoronaBadge>(p => p
            .Add(x => x.Dot, true)
            .Add(x => x.Position, CoronaBadgePosition.BottomLeft)
            .Add(x => x.Color, CoronaColorSemantic.Warning));

        var badge = cut.Find(".corona-badge");
        Assert.Contains("is-dot", badge.ClassList);
        Assert.Contains("corona-badge--bottomleft", badge.ClassList);
    }

    [Fact]
    public void CoronaCard_RendersHeaderAndLayoutStyles()
    {
        var cut = RenderComponent<CoronaCard>(p => p
            .Add(x => x.Title, "Title")
            .Add(x => x.ContentTextAlign, CoronaTextAlign.Justify)
            .Add(x => x.ContentJustify, CoronaFlexAlign.SpaceAround)
            .Add(x => x.ContentAlignItems, CoronaItemsAlign.Center)
            .AddChildContent("Body"));

        var style = cut.Find(".corona-card-content").GetAttribute("style");
        Assert.Contains("text-align:justify", style);
        Assert.Contains("justify-content:space-around", style);
        Assert.Contains("align-items:center", style);
    }

    [Fact]
    public void CoronaContainer_UsesRequestedMaxWidth()
    {
        var cut = RenderComponent<CoronaContainer>(p => p
            .Add(x => x.MaxWidth, CoronaContainerMaxWidth.Xs)
            .Add(x => x.CenterContent, true));

        var container = cut.Find(".corona-container");
        Assert.Contains("is-centered", container.ClassList);
        Assert.Contains("max-width:20rem", container.GetAttribute("style"));
    }

    [Fact]
    public void CoronaDialog_BackdropAndEscape_CloseWhenEnabled()
    {
        bool? open = true;
        var cut = RenderComponent<CoronaDialog>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .Add(x => x.Title, "Dialog")
            .AddChildContent("Body"));

        cut.Find(".corona-dialog__overlay").Click();
        Assert.False(open);

        open = true;
        cut.Find(".corona-dialog-shell").KeyDown(new KeyboardEventArgs { Key = "Escape" });
        Assert.False(open);
    }

    [Fact]
    public void CoronaDialog_CloseBlocked_WhenOptionsDisabled()
    {
        bool? open = true;
        var cut = RenderComponent<CoronaDialog>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.CloseOnBackdrop, false)
            .Add(x => x.CloseOnEscape, false)
            .Add(x => x.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .AddChildContent("Body"));

        cut.Find(".corona-dialog__overlay").Click();
        cut.Find(".corona-dialog-shell").KeyDown(new KeyboardEventArgs { Key = "Escape" });

        Assert.True(open);
    }

    [Fact]
    public void CoronaDrawer_RendersRightSideAndClosesOnKeyboard()
    {
        bool? open = true;
        var cut = RenderComponent<CoronaDrawer>(p => p
            .Add(x => x.Open, true)
            .Add(x => x.Position, CoronaDrawerPosition.Right)
            .Add(x => x.OpenChanged, EventCallback.Factory.Create<bool>(this, value => open = value))
            .AddChildContent("Nav"));

        Assert.Contains("corona-drawer--right", cut.Find("aside").ClassName);
        cut.Find(".corona-drawer-overlay").KeyDown(new KeyboardEventArgs { Key = "Enter" });
        Assert.False(open);
    }

    [Fact]
    public void CoronaNavMenu_RendersHierarchy_AndTogglesChildren()
    {
        Services.AddSingleton<NavigationManager>(new TestNavigationManager("http://localhost/", "http://localhost/docs"));

        var items = new List<CoronaNavItem>
        {
            new() { Text = "Home", Url = "/" },
            new() { Text = "Docs", Url = "/docs", Children = [new CoronaNavItem { Text = "Guide", Url = "/docs/guide" }] }
        };

        var cut = RenderComponent<CoronaNavMenu>(p => p.Add(x => x.Items, items));

        Assert.Contains("is-active", cut.Markup);
        cut.Find(".corona-nav-menu__expander").Click();
        Assert.Contains("Guide", cut.Markup);
    }

    [Fact]
    public void CoronaPageHeader_MenuInteractions_InvokeCallback()
    {
        var clicks = 0;
        var cut = RenderComponent<CoronaPageHeader>(p => p
            .Add(x => x.Title, "My Page")
            .Add(x => x.ShowMenuButton, true)
            .Add(x => x.Density, CoronaHeaderDensity.Spacious)
            .Add(x => x.OnMenuClick, EventCallback.Factory.Create(this, () => clicks++)));

        var button = cut.Find(".corona-page-header__menu-button");
        button.Click();
        button.KeyDown(new KeyboardEventArgs { Key = "Enter" });

        Assert.Equal(2, clicks);
    }

    [Fact]
    public void CoronaStack_MapsLayoutValues()
    {
        var cut = RenderComponent<CoronaStack>(p => p
            .Add(x => x.Direction, CoronaStackDirection.Row)
            .Add(x => x.Wrap, CoronaStackWrap.WrapReverse)
            .Add(x => x.Justify, CoronaStackJustify.SpaceEvenly)
            .Add(x => x.Align, CoronaStackAlign.Baseline)
            .Add(x => x.FullWidth, true));

        var style = cut.Find(".corona-stack").GetAttribute("style");
        Assert.Contains("flex-direction:row", style);
        Assert.Contains("flex-wrap:wrap-reverse", style);
        Assert.Contains("justify-content:space-evenly", style);
        Assert.Contains("align-items:baseline", style);
    }

    [Fact]
    public void CoronaTabs_RendersAndSwitchesActiveTab()
    {
        var changed = -1;

        var cut = RenderComponent<CoronaTabs>(p => p
            .Add(x => x.ActiveIndex, 0)
            .Add(x => x.ActiveIndexChanged, EventCallback.Factory.Create<int>(this, i => changed = i))
            .Add(x => x.LazyRender, true)
            .AddChildContent<TabsFixture>());

        var tabs = cut.FindAll(".corona-tabs__tab");
        Assert.Equal(2, tabs.Count);
        tabs[1].Click();

        Assert.Equal(1, changed);
    }

    [Fact]
    public void CoronaThemeCascadingValue_RendersChildContent()
    {
        var cut = RenderComponent<CoronaThemeCascadingValue>(p => p.AddChildContent("Themed content"));
        Assert.Contains("Themed content", cut.Markup);
    }

    [Fact]
    public void CoronaLayoutShell_RendersDrawerAndContent()
    {
        var items = new List<CoronaNavItem> { new() { Text = "Item", Url = "/" } };

        var cut = RenderComponent<CoronaLayoutShell>(p => p
            .Add(x => x.DrawerTitle, "Menu")
            .Add(x => x.ShowMenuButton, true)
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddContent(0, "Main content"))));

        Assert.Contains("Menu", cut.Markup);
        Assert.Contains("Main content", cut.Markup);
    }

    private sealed class TabsFixture : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CoronaTabItem>(0);
            builder.AddAttribute(1, nameof(CoronaTabItem.Header), "First");
            builder.AddAttribute(2, nameof(CoronaTabItem.ChildContent), (RenderFragment)(b => b.AddContent(0, "First body")));
            builder.CloseComponent();

            builder.OpenComponent<CoronaTabItem>(3);
            builder.AddAttribute(4, nameof(CoronaTabItem.Header), "Second");
            builder.AddAttribute(5, nameof(CoronaTabItem.ChildContent), (RenderFragment)(b => b.AddContent(0, "Second body")));
            builder.CloseComponent();
        }
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
    }
}
