using System.Data;

namespace Mordor.Infrastructure
{
	public class ConnectionManager : IDisposable
	{
		private readonly DapperDbContext _dbContext;
		private IDbConnection _connection;

		public ConnectionManager(DapperDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public IDbConnection Connection
		{
			get
			{
				if (_connection == null)
				{
					_connection = _dbContext.CreateConnection();
				}
				return _connection;
			}
		}
				
		public string GetConnectionString()
		{
			return _dbContext.GetConnectionString();
		}

		public void Dispose()
		{
			if (_connection != null)
			{
				_connection.Dispose();
				_connection = null;
			}
		}
	}
}