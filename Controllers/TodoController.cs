using Microsoft.AspNetCore.Mvc;
using TODO.Dtos;
using TODO.Models;
using TODO.Repository;

namespace TODO.Controllers;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly ITodoRepository _todoRepository;
    // private readonly IUserRepository _userRepository;

    public TodoController(ILogger<TodoController> logger, ITodoRepository todoRepository)
    {
        _logger = logger;
        _todoRepository = todoRepository;
        // _userRepository = userRepository;
    }

    [HttpGet]

    public async Task<ActionResult<List<TodoDto>>> GetAll()
    {
        var todoList = await _todoRepository.GetList();

        var dtoList = todoList; //.Select(x => x.asDto) ;
        return Ok(todoList);
    }
    [HttpGet("{todo_id}")]

    public async Task<ActionResult<TodoDto>> GetUserById([FromRoute] long todo_id)
    {
        var user = await _todoRepository.GetById(todo_id);

        if (user is null)
            return NotFound("No user found with given todo_id");

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateTodoDto Data)
    {
        var toCreateTodo = new Todo
        {
            Title = Data.Title.Trim(),
            Description = Data.Description.Trim(),
            CreatedAt = Data.CreatedAt.UtcDateTime,
            UpdatedAt = Data.UpdatedAt.UtcDateTime,
            DeletedOrNot = Data.DeletedOrNot,
            UserId = Data.UserId, //check
        };

        var res = await _todoRepository.Create(toCreateTodo);

        return StatusCode(StatusCodes.Status201Created, res);
    }

    [HttpPut("{todo_id}")]

    public async Task<ActionResult> UpdateUser([FromRoute] long todo_id,
    [FromBody] UpdateTodoDto Data)
    {
        var existing = await _todoRepository.GetById(todo_id);
        if (existing is null)
            return NotFound("No user found with given todo id");

        var toUpdateTodo = existing with
        {
            Title = Data.Title.Trim(),
            Description = Data.Description.Trim(),
        };

        var didUpdate = await _todoRepository.Update(toUpdateTodo);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update user");

        return NoContent();
    }

    [HttpDelete("{todo_id}")]

    public async Task<ActionResult> DeleteUser([FromRoute] long todo_id)
    {
        var existing = await _todoRepository.GetById(todo_id);

        if (existing is null)
            return NotFound("No user found with given user id");

        var didDelete = await _todoRepository.Delete(todo_id);

        return NoContent();

    }
    


}