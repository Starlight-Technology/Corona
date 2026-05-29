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
    public void CoronaLoading_Dots_RendersStatusAndLabel()
    {
        var cut = RenderComponent<CoronaLoading>(p => p
            .Add(x => x.Variant, CoronaLoadingVariant.Dots)
            .Add(x => x.Label, "Loading data")
            .Add(x => x.Color, CoronaColorSemantic.Success));

        var loading = cut.Find(".corona-loading");

        Assert.Equal("status", loading.GetAttribute("role"));
        Assert.Contains("corona-loading--dots", loading.ClassList);
        Assert.Contains("Loading data", cut.Markup);
    }

    [Fact]
    public void CoronaProgressBar_ClampsValueAndRendersFill()
    {
        var cut = RenderComponent<CoronaProgressBar>(p => p
            .Add(x => x.Label, "Upload")
            .Add(x => x.Value, 140));

        var track = cut.Find(".corona-progress__track");
        var fill = cut.Find(".corona-progress__fill");

        Assert.Equal("100", track.GetAttribute("aria-valuenow"));
        Assert.Contains("width:100%", fill.GetAttribute("style"));
        Assert.Contains("100%", cut.Markup);
    }

    [Fact]
    public void CoronaFileUpload_RendersConfiguredInput()
    {
        var cut = RenderComponent<CoronaFileUpload>(p => p
            .Add(x => x.Title, "Upload report")
            .Add(x => x.Accept, ".csv,.xlsx")
            .Add(x => x.Multiple, true));

        var input = cut.Find("input[type=file]");

        Assert.Equal(".csv,.xlsx", input.GetAttribute("accept"));
        Assert.True(input.HasAttribute("multiple"));
        Assert.Contains("Upload report", cut.Markup);
    }

    [Fact]
    public void CoronaChart_Bar_RendersDataAndLegend()
    {
        var data = CreateChartData();

        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.Title, "Revenue")
            .Add(x => x.Type, CoronaChartType.Bar)
            .Add(x => x.Data, data));

        Assert.Equal(2, cut.FindAll(".corona-chart__bar").Count);
        Assert.Equal(2, cut.FindAll(".corona-chart__legend li").Count);
        Assert.Contains("Revenue", cut.Markup);
        Assert.Contains("Q2", cut.Markup);
    }

    [Fact]
    public void CoronaChart_Line_RendersPathAndMarkers()
    {
        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.Type, CoronaChartType.Line)
            .Add(x => x.Data, CreateChartData()));

        Assert.NotNull(cut.Find(".corona-chart__line-path"));
        Assert.Equal(2, cut.FindAll(".corona-chart__line-point").Count);
        Assert.DoesNotContain("corona-chart__bar", cut.Markup);
    }

    [Fact]
    public void CoronaChart_Area_RendersFillLineAndMarkers()
    {
        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.Type, CoronaChartType.Area)
            .Add(x => x.Data, CreateChartData()));

        Assert.NotNull(cut.Find(".corona-chart__area-fill"));
        Assert.NotNull(cut.Find(".corona-chart__line-path"));
        Assert.Equal(2, cut.FindAll(".corona-chart__line-point").Count);
    }

    [Fact]
    public void CoronaChart_HorizontalBar_RendersHorizontalBars()
    {
        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.Type, CoronaChartType.HorizontalBar)
            .Add(x => x.Data, CreateChartData()));

        Assert.Equal(2, cut.FindAll(".corona-chart__horizontal-bar").Count);
        Assert.Contains("text-anchor=\"end\"", cut.Markup);
        Assert.Contains("Q1", cut.Markup);
    }

    [Fact]
    public void CoronaChart_Scatter_RendersPointsWithoutPath()
    {
        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.Type, CoronaChartType.Scatter)
            .Add(x => x.Data, CreateChartData()));

        Assert.Equal(2, cut.FindAll(".corona-chart__scatter-point").Count);
        Assert.Empty(cut.FindAll(".corona-chart__line-path"));
    }

    [Fact]
    public void CoronaChart_Pie_RendersSlices()
    {
        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.Type, CoronaChartType.Pie)
            .Add(x => x.Data, CreateChartData()));

        Assert.Equal(2, cut.FindAll(".corona-chart__pie-slice").Count);
        Assert.Empty(cut.FindAll(".corona-chart__donut-segment"));
    }

    [Fact]
    public void CoronaChart_Pie_SingleValueRendersFullCirclePath()
    {
        var data = new List<CoronaChartDataPoint> { new("All", 100) };

        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.Type, CoronaChartType.Pie)
            .Add(x => x.Data, data));

        var path = cut.Find(".corona-chart__pie-slice").GetAttribute("d");

        Assert.Contains("a 62 62 0 1 0", path);
    }

    [Fact]
    public void CoronaChart_Donut_RendersSegmentsAndTotal()
    {
        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.Type, CoronaChartType.Donut)
            .Add(x => x.Data, CreateChartData())
            .Add(x => x.TotalLabel, "orders"));

        Assert.Equal(2, cut.FindAll(".corona-chart__donut-segment").Count);
        Assert.Contains("corona-chart__donut-total", cut.Markup);
        Assert.Contains("orders", cut.Markup);
    }

    [Fact]
    public void CoronaChart_Empty_RendersEmptyState()
    {
        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.EmptyText, "Waiting")
            .Add(x => x.Data, Array.Empty<CoronaChartDataPoint>()));

        Assert.NotNull(cut.Find(".corona-chart__empty"));
        Assert.Contains("Waiting", cut.Markup);
        Assert.Empty(cut.FindAll("svg"));
    }

    [Fact]
    public void CoronaChart_ValueFormatter_FormatsVisibleValues()
    {
        var cut = RenderComponent<CoronaChart>(p => p
            .Add(x => x.Data, CreateChartData())
            .Add(x => x.ValueFormatter, value => $"{value:0} units"));

        Assert.Contains("12 units", cut.Markup);
        Assert.Contains("28 units", cut.Markup);
    }

    [Fact]
    public void CoronaProgressBar_Indeterminate_OmitsCurrentValue()
    {
        var cut = RenderComponent<CoronaProgressBar>(p => p
            .Add(x => x.Indeterminate, true)
            .Add(x => x.Label, "Analyzing")
            .Add(x => x.ShowValue, false));

        var progress = cut.Find(".corona-progress");
        var track = cut.Find(".corona-progress__track");

        Assert.Contains("is-indeterminate", progress.ClassList);
        Assert.Null(track.GetAttribute("aria-valuenow"));
        Assert.DoesNotContain("%", cut.Markup);
    }

    [Fact]
    public void CoronaLoading_Skeleton_RendersSkeletonLines()
    {
        var cut = RenderComponent<CoronaLoading>(p => p
            .Add(x => x.Variant, CoronaLoadingVariant.Skeleton)
            .Add(x => x.Inline, false));

        Assert.Contains("corona-loading--skeleton", cut.Find(".corona-loading").ClassList);
        Assert.Equal(3, cut.FindAll(".corona-loading__skeleton-line").Count);
    }

    [Fact]
    public void CoronaFileUpload_Disabled_RendersDisabledInputAndState()
    {
        var cut = RenderComponent<CoronaFileUpload>(p => p
            .Add(x => x.Disabled, true)
            .Add(x => x.Title, "Uploads locked"));

        Assert.True(cut.Find("input[type=file]").HasAttribute("disabled"));
        Assert.Contains("is-disabled", cut.Find(".corona-file-upload__dropzone").ClassList);
        Assert.Contains("Uploads locked", cut.Markup);
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

        cut.WaitForAssertion(() => Assert.Equal(2, cut.FindAll(".corona-tabs__tab").Count));

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
            .Add(x => x.DrawerItens, items)
            .Add(x => x.DrawerTitle, "Menu")
            .Add(x => x.HeaderTitle, "Header")
            .Add(x => x.ShowMenuButton, true)
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddContent(0, "Main content"))));

        Assert.Contains("Menu", cut.Markup);
        Assert.Contains("Main content", cut.Markup);
    }

    [Fact]
    public void CoronaLayoutShell_AcceptsNavItemsAlias()
    {
        var items = new List<CoronaNavItem> { new() { Text = "Alias item", Url = "/" } };

        var cut = RenderComponent<CoronaLayoutShell>(p => p
            .Add(x => x.NavItems, items)
            .Add(x => x.DrawerTitle, "Menu")
            .Add(x => x.HeaderTitle, "Header")
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddContent(0, "Main content"))));

        Assert.Contains("Alias item", cut.Markup);
    }

    [Fact]
    public void CoronaInput_Currency_RemovesTrailingTextFromExistingFormattedValue()
    {
        string currentValue = "R$ 55,22";

        var cut = RenderComponent<CoronaInput>(p => p
            .Add(x => x.Type, "currency")
            .Add(x => x.Value, currentValue)
            .Add(x => x.ValueChanged, EventCallback.Factory.Create<string>(this, value => currentValue = value))
            .Add(x => x.ValueExpression, () => currentValue));

        var input = cut.Find("input");
        Assert.Equal("R$ 55,22", input.GetAttribute("value"));

        input.Change("R$ 55,22 uwehifw");

        cut.WaitForAssertion(() => Assert.Equal("R$ 55,22", cut.Find("input").GetAttribute("value")));
        Assert.Equal("5522", currentValue);
    }

    private static IReadOnlyList<CoronaChartDataPoint> CreateChartData() =>
    [
        new("Q1", 12),
        new("Q2", 28, "#0F766E")
    ];

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
