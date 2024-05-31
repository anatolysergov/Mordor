using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Mordor.Infrastructure.Data;

public class DataContext : DbContext
{
	private readonly string _connectionString;

		public DataContext(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}
}