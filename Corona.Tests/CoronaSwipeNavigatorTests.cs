using Bunit;
using Corona.Components;
using Corona.Components.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Corona.Tests;

public sealed class CoronaSwipeNavigatorTests : ComponentTestContext
{
    [Fact]
    public void LeftSwipe_NavigatesToNextPage()
    {
        var navigation = RegisterNavigation("http://localhost/income?month=05");

        var cut = RenderComponent<CoronaSwipeNavigator>(p => p
            .Add(x => x.Items, MenuItems())
            .Add(x => x.SwipeThresholdPx, 50)
            .AddChildContent("Page content"));

        Swipe(cut, startX: 250, endX: 120, startY: 40, endY: 46);

        Assert.Equal("http://localhost/outcome", navigation.Uri);
    }

    [Fact]
    public void RightSwipe_NavigatesToPreviousPage()
    {
        var navigation = RegisterNavigation("http://localhost/outcome");

        var cut = RenderComponent<CoronaSwipeNavigator>(p => p
            .Add(x => x.Items, MenuItems())
            .Add(x => x.SwipeThresholdPx, 50)
            .AddChildContent("Page content"));

        Swipe(cut, startX: 120, endX: 250, startY: 40, endY: 46);

        Assert.Equal("http://localhost/income", navigation.Uri);
    }

    [Fact]
    public void Swipe_DoesNotNavigate_WhenMovementIsShortOrVertical()
    {
        var navigation = RegisterNavigation("http://localhost/income");

        var cut = RenderComponent<CoronaSwipeNavigator>(p => p
            .Add(x => x.Items, MenuItems())
            .Add(x => x.SwipeThresholdPx, 50)
            .AddChildContent("Page content"));

        Swipe(cut, startX: 250, endX: 220, startY: 40, endY: 42);
        Assert.Equal("http://localhost/income", navigation.Uri);

        Swipe(cut, startX: 250, endX: 170, startY: 40, endY: 180);
        Assert.Equal("http://localhost/income", navigation.Uri);
    }

    [Fact]
    public void Swipe_DoesNotNavigate_WhenPointerIsCancelled()
    {
        var navigation = RegisterNavigation("http://localhost/income");

        var cut = RenderComponent<CoronaSwipeNavigator>(p => p
            .Add(x => x.Items, MenuItems())
            .Add(x => x.SwipeThresholdPx, 50)
            .AddChildContent("Page content"));

        var container = cut.Find(".swipe-container");
        container.TriggerEvent("onpointerdown", new PointerEventArgs { ClientX = 250, ClientY = 40 });
        container.TriggerEvent("onpointercancel", new PointerEventArgs { ClientX = 250, ClientY = 40 });
        container.TriggerEvent("onpointerup", new PointerEventArgs { ClientX = 120, ClientY = 46 });

        Assert.Equal("http://localhost/income", navigation.Uri);
    }

    private TestNavigationManager RegisterNavigation(string uri)
    {
        var navigation = new TestNavigationManager("http://localhost/", uri);
        Services.AddSingleton<NavigationManager>(navigation);
        return navigation;
    }

    private static void Swipe(IRenderedComponent<CoronaSwipeNavigator> cut, double startX, double endX, double startY, double endY)
    {
        var container = cut.Find(".swipe-container");
        container.TriggerEvent("onpointerdown", new PointerEventArgs { ClientX = startX, ClientY = startY });
        container.TriggerEvent("onpointerup", new PointerEventArgs { ClientX = endX, ClientY = endY });
    }

    private static List<CoronaNavItem> MenuItems() =>
    [
        new() { Text = "Dashboard", Url = "/dashboard" },
        new()
        {
            Text = "Transactions",
            Children =
            [
                new CoronaNavItem { Text = "Income", Url = "/income" },
                new CoronaNavItem { Text = "Outcome", Url = "/outcome" },
                new CoronaNavItem { Text = "Entries", Url = "/entries" }
            ]
        }
    ];

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
