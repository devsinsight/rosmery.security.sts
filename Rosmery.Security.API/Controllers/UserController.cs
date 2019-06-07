using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rosmery.Security.API.Commands;
using Rosmery.Security.API.Models;
using Rosmery.Security.Identity.Managers;
using Rosmery.Security.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rosmery.Security.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SECURITY_ADMINISTRATOR")]
    public class UserController : ControllerBase
    {

        private readonly SecurityUserManager<User> _userManager;
        private readonly SecurityRoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private const string SECURITY_ADMINISTRATOR = "SECURITY_ADMINISTRATOR";

        public UserController(
            SecurityUserManager<User> userManager,
            SecurityRoleManager<Role> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet("GetUsers")]
        public List<UserModel> GetUsers()
        {
            return _userManager
                        .GetAllUsers()
                        .Select(u => new UserModel()
                        {
                            UserId = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Email = u.Email,
                            UserName = u.UserName,
                            PhoneNumber = u.PhoneNumber,
                            Role = _userManager
                                        .GetRolesAsync(u).Result?
                                        .Select(r => _roleManager.FindByNameAsync(r).Result)
                                        .FirstOrDefault()
                        })
                        .Where(u => u.Role?.Name != SECURITY_ADMINISTRATOR)
                        .ToList();

        }

        [HttpPost("CreateUser")]
        public async Task CreateUser(CreateUserCommand command)
        {
            ValidateUserNameOnCreate(command.UserName);

            var user = _mapper.Map<CreateUserCommand, User>(command);
            user.EmailConfirmed = true;
            await _userManager.CreateAsync(user);

            if (command.RoleId != null) { 
                var role = await _roleManager.FindByIdAsync(command.RoleId);
                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }

        
        private void ValidateUserNameOnCreate(string userName) {
            if (_userManager.UserNameExists(userName))
                throw new Exception(string.Format("UserName {0} already exists.", userName));
        }

        [HttpGet("ValidateUserName/{userName}")]
        public bool ValidateUserName(string userName) {
            return _userManager.UserNameExists(userName);
        }

        [HttpPut("UpdateUser")]
        public async Task CreateUser(UpdateUserCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.UserId);
            var role = await _roleManager.FindByIdAsync(command.RoleId);
            var oldRoleNames = await _userManager.GetRolesAsync(user);

            if (IsEqualUser(command, user) && role.Name == oldRoleNames[0]) return;

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.PhoneNumber = command.PhoneNumber;
            user.Email = command.Email;

            await _userManager.UpdateAsync(user);
            
            await _userManager.RemoveFromRoleAsync(user, oldRoleNames[0]);
            if (command.RoleId != null)
                await _userManager.AddToRoleAsync(user, role.Name);

        }

        private bool IsEqualUser(UpdateUserCommand x, User y) {
            return x.FirstName == y.FirstName &&
                    x.LastName == y.LastName &&
                    x.PhoneNumber == y.PhoneNumber &&
                    x.Email == y.Email;
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task DeleteUser(string userId) {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRoleAsync(user, roles[0]);
            await _userManager.DeleteAsync(user);
        }
    }


}