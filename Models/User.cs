using TODO.Dtos;

namespace TODO.Models
{

public record User
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public long Mobile  { get; set; }
    public string Address { get; set; }


    public UserDto asDto()=> new UserDto
    {

        UserId = UserId,
        Name =Name,
        Password = Password,
        Email = Email,
        Mobile = Mobile,
        Address = Address,

    };
}
}
