using Dapper;
using Mordor.Infrastructure.Data;

namespace Mordor.Infrastructure.Repositories
{
	public class BaseRepository
	{
		private readonly DapperDbContext _context;
		private readonly ConnectionManager _connectionManager;

		public BaseRepository(DapperDbContext context, ConnectionManager connectionManager)
		{
			_context = context;
			_connectionManager = connectionManager;
		}

		public async Task<T> Get<T>(string sql, object parameters = null)
		{
			return await _connectionManager.Connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
		}

		public async Task<IEnumerable<T>> GetAll<T>(string sql, object parameters = null)
		{
			return await _connectionManager.Connection.QueryAsync<T>(sql, parameters);
		}

		public async Task<T> Create<T>(string sql, object parameters = null)
		{
			return await _connectionManager.Connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
		}

		public async Task<T> Update<T>(string sql, object parameters = null)
		{
			return await _connectionManager.Connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
		}

		public async Task<T> Delete<T>(string sql, object parameters = null)
		{
			return await _connectionManager.Connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
		}
	}
}