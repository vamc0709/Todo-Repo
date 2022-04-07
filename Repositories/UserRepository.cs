using TODO.Dtos;
using TODO.Models;

namespace TODO.Repository;

using Dapper;
using TODO.Utilities;

public interface IUserRepository
{
    Task<User> Create(User Item);
    Task<bool> Update(User Item);
    Task<bool> Delete(long UserId);
    Task<User> GetById(long UserId);
    Task<List<User>> GetList();
}

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<User> Create(User Item)
    {
        var query = $@"INSERT INTO ""{TableNames.user}""(name, password, email, mobile, address) VALUES (@Name, @Password, @Email, @Mobile, @Address)
        RETURNING *";
        using (var connection = NewConnection)
        {
           var res = await connection.QuerySingleOrDefaultAsync<User>(query, Item);
              return res;
        }
    }

    public async Task<bool> Delete(long UserId)
    {
        var query = $@"DELETE FROM ""{TableNames.user}"" WHERE user_id = @UserId";
        using (var connection = NewConnection)
        {
            var res = await connection.ExecuteAsync(query, new { UserId });
            return res > 0;
        }
    }

    public async Task<User> GetById(long UserId)
    {
        var query = $@"SELECT * FROM ""{TableNames.user}"" WHERE user_id = @UserId";
        using (var connection = NewConnection)
        {
            return await connection.QuerySingleOrDefaultAsync<User>(query, new { UserId });
            
        }
    }

    public async Task<List<User>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.user}""";
        List<User> result;
        using (var connection = NewConnection)
        
        result = (await connection.QueryAsync<User>(query)).AsList();
        return result;
    }

    public async Task<bool> Update(User Item)
    {
        var query = $@"UPDATE ""{TableNames.user}"" SET Name = @Name, Password = @Password, Email = @Email, Mobile = @Mobile, Address = @Address WHERE user_id = @UserId";
        using (var connection = NewConnection)
        {
            var rowCount = await connection.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }
}