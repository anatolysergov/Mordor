namespace Mordor.Domain.Entities
{
	public class AppUser
	{
		public int Id { get; set; }
		public Guid UserId { get; set;}
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}