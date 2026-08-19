using Microsoft.AspNetCore.Components;

namespace Corona.Components.Models;

public sealed class CoronaNavItem
{
    public required string Text { get; init; }

    public string? Icon { get; init; }

    public string? Url { get; init; }

    public IReadOnlyList<CoronaNavItem> Children { get; init; } = [];

    public bool Expanded { get; set; }

    public RenderFragment? Content { get; init; }

    // When true the item should only be displayed on small screens (e.g. in-header small area)
    // and hidden on large screens. Defaults to false.
    public bool ShowOnlyOnSmall { get; init; } = false;
}
