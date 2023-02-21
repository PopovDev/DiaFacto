using System.Text.Json.Serialization;

namespace DiaFacto.Models;

[Serializable]
public class User
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("surname")]
    public string? Surname { get; set; }
    [JsonPropertyName("avatar_image")]
    public string? AvatarImage { get; set; }
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }
    [JsonPropertyName("mails")]
    public required string[] Mails { get; set; }
    
}