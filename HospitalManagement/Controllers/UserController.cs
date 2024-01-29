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
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }
        [HttpPost,Route("list")]
        public async Task<IActionResult> UserList([FromBody] JqueryDataTable userList)
        {
            try
            {
                var res = await _userRepository.UserList(userList);
                return res.Status ? StatusCode(StatusCodes.Status200OK, res) : StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }

        [HttpPost, Route("update")]
        public async Task<IActionResult> UserUpdate([FromBody] UserUpdate userUpdate)
        {
            try
            {
                var responce = await _userRepository.UserUpdate(userUpdate);

                return responce.Status ? StatusCode(StatusCodes.Status200OK): StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpGet,Route("get")]

        public async Task<IActionResult> UserGet(string UserId)
        {
            try
            {
                var responce = await _userRepository.UserGet(UserId); 
                return responce.Status ? StatusCode(StatusCodes.Status200OK,responce) : StatusCode(StatusCodes.Status400BadRequest,responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpDelete, Route("delete")]
        public async Task<IActionResult> UserDelete(string UserId)
        {
            try
            {
                var responce = await _userRepository.UserDelete(UserId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK,responce) : StatusCode(StatusCodes.Status400BadRequest,responce);
            }
            catch 
            {

                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }

    }
}
