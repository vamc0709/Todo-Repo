using System.Text.Json.Serialization;

namespace TODO.Dtos;

public record TodoDto
{
    [JsonPropertyName("todo_id")]
    public long TodoId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set;}

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("deleted_or_not")]
    public bool DeletedOrNot { get; set; }

    [JsonPropertyName("user_id")]

    public long UserId { get; set; }

}
public record CreateTodoDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    [JsonPropertyName("deleted_or_not")]
    public bool DeletedOrNot { get; set; }
 
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }
}
public record UpdateTodoDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
}