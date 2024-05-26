using Dapper;
using Mordor.Domain.Entities;
using Mordor.Domain.Interfaces;
using Mordor.Infrastructure.Data;

namespace Mordor.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _context;

        public UserRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserById(Guid userId)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "SELECT * FROM Users WHERE UserId = @UserId";
                return await connection.QueryFirstOrDefaultAsync<AppUser>(sql, new { UserId = userId });
            }
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "SELECT * FROM Users WHERE Username = @Username";
                return await connection.QueryFirstOrDefaultAsync<AppUser>(sql, new { Username = username });
            }
        }

        public async Task CreateUser(AppUser user)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "INSERT INTO Users (UserId, UserName, Password) VALUES (@UserId, @UserName, @Password)";
                await connection.ExecuteAsync(sql, user);
            }
        }

        public async Task UpdateUser(AppUser user)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "UPDATE Users SET UserName = @UserName, Password = @Password WHERE UserId = @UserId";
                await connection.ExecuteAsync(sql, user);
            }
        }

        public async Task DeleteUser(Guid id)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "DELETE FROM Users WHERE UserId = @UserId";
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
