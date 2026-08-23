using Corona.Components;
using Corona.Components.Models;

namespace BlazorApp.Components.Navigation;

public static class DocsNavigation
{
    public static IReadOnlyList<CoronaNavItem> Items { get; } =
    [
        new CoronaNavItem
        {
            Text = "Getting Started",
            Icon = "🚀",
            Expanded = true,
            Children =
            [
                new CoronaNavItem { Text = "Overview", Url = "/" }
            ]
        },
        new CoronaNavItem
        {
            Text = "Layout",
            Icon = "📐",
            Expanded = true,
            Children =
            [
                new CoronaNavItem { Text = "Container", Url = "/components/container" },
                new CoronaNavItem { Text = "Stack", Url = "/components/stack" },
                new CoronaNavItem { Text = "Drawer + Header Island", Url = "/components/drawer" }
            ]
        },
        new CoronaNavItem
        {
            Text = "Inputs",
            Icon = "⌨️",
            Expanded = true,
            Children =
            [
                new CoronaNavItem { Text = "Button", Url = "/components/button" },
                new CoronaNavItem { Text = "Input", Url = "/components/input" },
                new CoronaNavItem { Text = "File Upload", Url = "/components/file-upload" }
            ]
        },
        new CoronaNavItem
        {
            Text = "Data Display",
            Icon = "📊",
            Expanded = true,
            Children =
            [
                new CoronaNavItem { Text = "Badge", Url = "/components/badge" },
                new CoronaNavItem { Text = "Card", Url = "/components/card" },
                new CoronaNavItem { Text = "Chart", Url = "/components/chart" },
                new CoronaNavItem { Text = "Icon", Url = "/components/icon" }
            ]
        },
        new CoronaNavItem
        {
            Text = "Feedback",
            Icon = "💬",
            Expanded = true,
            Children =
            [
                new CoronaNavItem { Text = "Dialog", Url = "/components/dialog" },
                new CoronaNavItem { Text = "Loading", Url = "/components/loading" },
                new CoronaNavItem { Text = "Progress", Url = "/components/progress" }
            ]
        },
        new CoronaNavItem
        {
            Text = "Navigation",
            Icon = "🧭",
            Expanded = true,
            Children =
            [
                new CoronaNavItem { Text = "Tabs", Url = "/components/tabs" }
            ]
        }
    ];
}
