using TODO.Dtos;

namespace TODO.Models;

public record Todo
{
    public long TodoId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; } 
    public DateTimeOffset UpdatedAt { get; set; }
    public bool DeletedOrNot { get; set; }
    public long UserId { get; set; }


    public TodoDto asDto() => new TodoDto
    {
        TodoId = TodoId,
        Title = Title,
        Description = Description,
        CreatedAt = CreatedAt,
        UpdatedAt = UpdatedAt,
        DeletedOrNot = DeletedOrNot,
        UserId = UserId,
    };
    
}