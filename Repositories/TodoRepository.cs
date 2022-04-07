namespace TODO.Repository;

using Dapper;
using TODO.Models;
using TODO.Utilities;

public interface ITodoRepository
{
    Task<Todo> Create(Todo Item);
    Task<bool> Update(Todo Item);
    Task<bool> Delete(long TodoId);
    Task<Todo> GetById(long TodoId);
    Task<List<Todo>> GetList();
}

public class TodoRepository : BaseRepository, ITodoRepository
{
    public TodoRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<Todo> Create(Todo Item)
    {
        var query = $@"INSERT INTO ""{TableNames.todo}"" (title, description, created_at, updated_at,deleted_or_not,user_id)
         VALUES ( @Title, @Description, @CreatedAt, @UpdatedAt, @DeletedOrNot,@UserId)
        RETURNING *";

        using (var connection = NewConnection)
        {
            var res = await connection.QuerySingleOrDefaultAsync<Todo>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long TodoId)
    {
       var query = $@"DELETE FROM todo WHERE todo_id = @TodoId";
        using (var connection = NewConnection)
        {
            var res = await connection.ExecuteAsync(query, new { TodoId });
            return res > 0;
        } 
    }

    public async Task<Todo> GetById(long TodoId)
    {
        var query = $@"SELECT * FROM ""{TableNames.todo}"" WHERE todo_id = @TodoId";
        using (var connection = NewConnection)
        
        return await connection.QuerySingleOrDefaultAsync<Todo>(query, new { TodoId });

    }

    public async Task<List<Todo>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.todo}""";
        List<Todo> result;
        using (var connection = NewConnection)
        
        result = (await connection.QueryAsync<Todo>(query)).AsList();
        return result;
    }

    public async Task<bool> Update(Todo Item)
    {
        var query = $@"UPDATE ""{TableNames.todo}"" SET title = @Title, description = @Description, deleted_or_not = @DeletedOrNot WHERE todo_id = @TodoId";
        using (var connection = NewConnection)
        {
            var rowCount = await connection.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }
}