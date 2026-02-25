using Microsoft.AspNetCore.Components;

namespace Corona.Components.Models;

internal sealed record CoronaTabDefinition(
    string Id,
    string Header,
    RenderFragment? HeaderContent,
    RenderFragment? ChildContent,
    bool Disabled,
    string? Class,
    string? PanelClass);
