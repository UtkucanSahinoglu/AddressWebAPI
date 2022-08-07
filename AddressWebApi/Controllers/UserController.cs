using AddressWebApi.Configuretion;
using AddressWebApi.Interface;
using AddressWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService,ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("AddUser")]
        public void Add(User user)
        {
            _logger.LogInformation("UserAdd method is called");
            _userService.Add(user);
        }
        [HttpDelete("DeleteUser")]
        public void Delete(User user)
        {
            _logger.LogInformation("DeleteUser method is called");
            _userService.Delete(user);
        }
        [HttpDelete("DeleteUserById")]
        public async Task<User> DeleteById(int Id)
        {
            _logger.LogInformation("DeleteUserById method is called");
             return await _userService.DeleteById(Id);
        }

        [HttpGet("AllUser")]
        public async Task<IEnumerable<User>> GetAll()
        {
            _logger.LogInformation("UserGetAll method is called");
            return await _userService.GetAll();
        }
        [HttpGet("GetByIdUser")]
        public async Task<User> GetById(int Id)
        {
            _logger.LogInformation("GetByIdUser method is called");
            return await _userService.GetById(Id);
        }
        [HttpPut("UpdateUser")]
        public void Update(User user)
        {
            _logger.LogInformation("UpdateUser method is called");
            _userService.Update(user);
        }
        [HttpGet("GetByUserFirstNameAndLastName")]
        public  async Task <IEnumerable<User>> GetByUserFirstNameAndLastName(string FirstName, string LastName)
        {
           return await  _userService.GetByUserFirstNameAndLastName(FirstName, LastName);
        }

    }
}
