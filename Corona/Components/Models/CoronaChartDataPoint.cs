namespace Corona.Components.Models;

public sealed record CoronaChartDataPoint(
    string Label,
    double Value,
    string? Color = null);
