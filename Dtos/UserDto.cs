using System.Text.Json.Serialization;

namespace TODO.Dtos;

public record UserDto
{
    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }


    public class CreateUserDto
    {
        // [JsonPropertyName("user_id")]
        // public long UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("mobile")]
        public long Mobile { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }


    public class UpdateUserDto
    {
        // [JsonPropertyName("user_id")]
        // public long UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("mobile")]
        public long Mobile { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }
}