using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Mordor.Infrastructure
{
	public class DapperDbContext
	{
		private readonly string _connectionString;

		public DapperDbContext(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}
		public IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}
		
		public string GetConnectionString()
		{
			return _connectionString;
		}
		
	}
}