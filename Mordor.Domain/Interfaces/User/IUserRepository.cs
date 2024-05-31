using Mordor.Domain.Entities;

namespace Mordor.Domain.Interfaces
{
	public interface IUserRepository
	{
		Task<AppUser> GetUserById(Guid id);
		Task<AppUser> GetUserByUsername(string username);
		Task CreateUser(AppUser user);
		Task UpdateUser(AppUser user);
		Task DeleteUser(Guid id);
	}
}
