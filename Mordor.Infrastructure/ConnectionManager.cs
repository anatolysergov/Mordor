using System.Data;
using Mordor.Infrastructure.Data;

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

        public IDbConnection Connection => _connection ??= _dbContext.CreateConnection();

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