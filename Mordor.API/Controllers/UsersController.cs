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

		public UsersController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
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
			if (id != user.UserId)
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
		public async Task<IActionResult> CreateNewUser([FromBody] UserCreateNewRequest userCreateNewRequest)
		{
			if (userCreateNewRequest == null)
			{
				return BadRequest();
			}
			
			var foundUser = _userRepository.GetUserByUsername(userCreateNewRequest.UserName);
			
			if (foundUser != null)
			{
				return Conflict();
			}
			
			var user = new AppUser
			{
				UserName = userCreateNewRequest.UserName,
				Password = userCreateNewRequest.Password,
				UserId = Guid.NewGuid()
			};
			
			await _userRepository.CreateUser(user);
			return CreatedAtAction("GetUser", new { id = user.UserId }, user);
		}
	}
}
