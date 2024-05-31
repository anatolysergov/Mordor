namespace Mordor.Domain.Entities
{
	public class AppUser
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
		public int UserRoleId { get; set; }
		public string CompanyName { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? DateModified { get; set; }
		public DateTime? LastLoggedIn { get; set; }
	}
}