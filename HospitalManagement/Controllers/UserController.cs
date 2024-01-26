using HospitalManagement.Core.Interface;
using HospitalManagement.Core.Model;
using HospotalManagement.Generic.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userRepository;
        private readonly IConfiguration _configuration;
        public UserController(IUserInterface userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            
        }
        [HttpPost, Route("insert")]
        public async Task<IActionResult> UserInsert([FromBody] UserInsert userInsert)
        {

            
            try
            {
                Authenticates.CreatePasswordHash(userInsert.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userInsert.PasswordHash = passwordHash;
                userInsert.PasswordSalt = passwordSalt;

                var res = await _userRepository.UserInsert(userInsert);

                return res.Status ? StatusCode(StatusCodes.Status201Created, res) : StatusCode(StatusCodes.Status409Conflict, res);
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }
    }
}
