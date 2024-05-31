namespace Mordor.API.DTOs
{
	public class UserCreateNewRequest
	{
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string CompanyName { get; set; }
	}
}