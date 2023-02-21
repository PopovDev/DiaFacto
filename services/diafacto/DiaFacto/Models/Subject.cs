using System.Text.Json.Serialization;

namespace DiaFacto.Models;

[Serializable]
public class Subject
{
    public required string Id { get; set; }
    [JsonPropertyName("title")]
    public required string Title { get; set; }
    [JsonPropertyName("full_name")]
    public string? FullName { get; set; }
    [JsonPropertyName("teacher")]
    public string? Teacher { get; set; }
    [JsonPropertyName("info")]
    public string Info { get; set; } = "";
    [JsonPropertyName("platform")]
    public string Platform { get; set; } = "";
    [JsonPropertyName("link")]
    public string Link { get; set; } = "";
}