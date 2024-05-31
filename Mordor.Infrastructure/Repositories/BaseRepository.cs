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
	}
}