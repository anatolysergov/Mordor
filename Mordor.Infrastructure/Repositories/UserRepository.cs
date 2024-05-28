using Dapper;
using Mordor.Domain.Entities;
using Mordor.Domain.Interfaces;
using Mordor.Infrastructure.Data;

namespace Mordor.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DapperDbContext _context;
		private readonly ConnectionManager _connectionManager;

		public UserRepository(DapperDbContext context, ConnectionManager connectionManager)
		{
			_context = context;
			_connectionManager = connectionManager;
		}

		public async Task<AppUser> GetUserById(Guid userId)
		{
			var sql = "SELECT * FROM Users WHERE UserId = @UserId";
			return await _connectionManager.Connection.QueryFirstOrDefaultAsync<AppUser>(sql, new { UserId = userId });
		}

		public async Task<AppUser> GetUserByUsername(string username)
		{

			var sql = "SELECT * FROM Users WHERE Username = @Username";
			return await _connectionManager.Connection.QueryFirstOrDefaultAsync<AppUser>(sql, new { Username = username });
		}

		public async Task CreateUser(AppUser user)
		{

			var sql = "INSERT INTO Users (UserId, UserName, Password) VALUES (@UserId, @UserName, @Password)";
			await _connectionManager.Connection.ExecuteAsync(sql, user);
		}

		public async Task UpdateUser(AppUser user)
		{

			var sql = "UPDATE Users SET UserName = @UserName, Password = @Password WHERE UserId = @UserId";
			await _connectionManager.Connection.ExecuteAsync(sql, user);
		}

		public async Task DeleteUser(Guid id)
		{

			var sql = "DELETE FROM Users WHERE UserId = @UserId";
			await _connectionManager.Connection.ExecuteAsync(sql, new { Id = id });
		}
	}
}
