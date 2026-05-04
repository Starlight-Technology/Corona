namespace Corona.Components.Models;

public class CoronaChatMessage
{
    public string Content { get; set; } = string.Empty;
    public bool IsReasoning { get; set; }
    public bool IsUser { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}