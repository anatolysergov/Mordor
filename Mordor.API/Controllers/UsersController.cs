using Microsoft.AspNetCore.Mvc;
using Mordor.Domain.Interfaces;
using Mordor.Domain.Entities;
using Mordor.API.DTOs;

namespace Mordor.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly ILogger<UsersController> Logger;

		public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
		{
			_userRepository = userRepository;
			Logger = logger;
		}

		[HttpGet("username/{username}")]
		public async Task<ActionResult<AppUser>> GetUserByUserName([FromRoute] string username)
		{
			var foundUser = await _userRepository.GetUserByUsername(username);
			if (foundUser == null)
			{
				return NotFound();
			}
			
			var user = new AppUser
			{
				Id = foundUser.Id,
				UserName = foundUser.UserName,
				Password = foundUser.Password
			};
			return Ok(user);
		}

		[HttpGet("{userId}")]
		public async Task<ActionResult<AppUser>> GetUserById(Guid userId)
		{
			var user = await _userRepository.GetUserById(userId);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		// TODO: Implement the UpdateUser method 
		// USE DTO UserUpdateReqeust
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(Guid id, AppUser user)
		{
			if (id.ToString() != user.UserId)
			{
				return BadRequest();
			}

			var existingUser = await _userRepository.GetUserById(id);
			if (existingUser == null)
			{
				return NotFound();
			}

			await _userRepository.UpdateUser(user);
			return NoContent();
		}
		
		[HttpPost]
		public async Task<IActionResult> RegisterNewUser([FromBody] UserCreateNewRequest userCreateNewRequest)
		{
			if (userCreateNewRequest == null)
			{
				return BadRequest("Request body is null.");
			}

			try
			{
				var foundUser = await _userRepository.GetUserByUsername(userCreateNewRequest.UserName);

				if (foundUser != null)
				{
					return Conflict("A user with the same username already exists.");
				}

				var user = new AppUser
				{
					UserName = userCreateNewRequest.UserName,
					Password = userCreateNewRequest.Password,
					UserId = Guid.NewGuid().ToString(),
					FirstName = userCreateNewRequest.FirstName,
					LastName = userCreateNewRequest.LastName,
					Email = userCreateNewRequest.Email,
					IsActive = false,
					UserRoleId = 1,
					CompanyName = userCreateNewRequest.CompanyName,
					CreatedDate = DateTime.Now,
					DateModified = DateTime.Now,
					LastLoggedIn = DateTime.Now,
				};

				await _userRepository.CreateUser(user);
				return Ok(user);
			}
			catch (Exception ex)
			{
				// Log the exception (ex)
				Logger.LogError(ex, "An error occurred while creating the user.");
				return StatusCode(500, "An error occurred while creating the user.");
			}
		}


	}
}
