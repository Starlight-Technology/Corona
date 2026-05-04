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

}
