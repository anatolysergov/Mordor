using Dapper;
using Microsoft.Data.SqlClient;
using Mordor.Domain.Entities;
using Mordor.Domain.Interfaces;
using Mordor.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Mordor.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly ConnectionManager _connectionManager;
		private readonly CreateNewUser _createNewUser;

		public UserRepository(ConnectionManager connectionManager, CreateNewUser createNewUser)
		{
			_connectionManager = connectionManager;
			_createNewUser = createNewUser;
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
			await _createNewUser.ExecuteCreateNewUser(user);
		}

        public Task UpdateUser(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        // public async Task UpdateUser(AppUser user)
        // {

        // 	var sql = "UPDATE Users SET UserName = @UserName, Password = @Password WHERE UserId = @UserId";
        // 	await _connectionManager.Connection.ExecuteAsync(sql, user);
        // }

        // public async Task DeleteUser(Guid id)
        // {

        // 	var sql = "DELETE FROM Users WHERE UserId = @UserId";
        // 	await _connectionManager.Connection.ExecuteAsync(sql, new { Id = id });
        // }
    }
}
