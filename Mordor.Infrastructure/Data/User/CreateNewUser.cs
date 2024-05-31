using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Mordor.Domain.Entities;

namespace Mordor.Infrastructure
{
	public class CreateNewUser
	{
		private readonly ConnectionManager _connectionManager;
		private readonly ILogger<CreateNewUser> _logger;
		public CreateNewUser(ConnectionManager connectionManager, ILogger<CreateNewUser> logger)
		{
			_connectionManager = connectionManager;
			_logger = logger;
		}
		
		public async Task ExecuteCreateNewUser(AppUser user)
		{
			string conString = _connectionManager.GetConnectionString();
			try
			{
				using (SqlConnection con = new SqlConnection(conString))
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "dbo.DoThing";
					cmd.Connection = con;

					cmd.Parameters.Add("@UserId", System.Data.SqlDbType.UniqueIdentifier).Value = Guid.Parse(user.UserId);
					cmd.Parameters.Add("@UserName", System.Data.SqlDbType.NVarChar).Value = user.UserName ?? (object)DBNull.Value;
					cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = user.Password ?? (object)DBNull.Value;
					cmd.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar).Value = user.FirstName ?? (object)DBNull.Value;
					cmd.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar).Value = user.LastName ?? (object)DBNull.Value;
					cmd.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = user.Email ?? (object)DBNull.Value;
					cmd.Parameters.Add("@IsActive", System.Data.SqlDbType.Bit).Value = user.IsActive;
					cmd.Parameters.Add("@UserRoleId", System.Data.SqlDbType.Int).Value = user.UserRoleId;
					cmd.Parameters.Add("@CompanyName", System.Data.SqlDbType.NVarChar).Value = user.CompanyName ?? (object)DBNull.Value;
					cmd.Parameters.Add("@CreatedDate", System.Data.SqlDbType.DateTime).Value = user.CreatedDate ?? (object)DBNull.Value;
					cmd.Parameters.Add("@DateModified", System.Data.SqlDbType.DateTime).Value = user.DateModified ?? (object)DBNull.Value;
					cmd.Parameters.Add("@LastLoggedIn", System.Data.SqlDbType.DateTime).Value = user.LastLoggedIn ?? (object)DBNull.Value;

					await con.OpenAsync();
					var returnValue = new SqlParameter("@ReturnValue", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.ReturnValue };
					cmd.Parameters.Add(returnValue);

					await cmd.ExecuteNonQueryAsync();

					int result = (int)returnValue.Value;
					if (result != 0)
					{
						_logger.LogError("Stored procedure {StoredProcedure} did not execute as expected. Result: {Result}", "dbo.DoThing", result);
						throw new Exception("Error executing stored procedure.");
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while executing the stored procedure {StoredProcedure}.", "dbo.DoThing");
				throw;
			}
		}
	}
}