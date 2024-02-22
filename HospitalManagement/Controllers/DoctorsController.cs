using HospitalManagement.Core.Interface;
using HospitalManagement.Core.Model;
using HospotalManagement.Generic.Helper;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepositories _doctorRepositories;
        private readonly IConfiguration _configuration;
        public DoctorsController(IConfiguration configuration, IDoctorRepositories doctorRepositories)
        {
            _doctorRepositories = doctorRepositories;
             _configuration = configuration;

        }
        [HttpPost, Route("insert")]
        public async Task<IActionResult> DoctorInsert([FromBody] DoctorInsert userInsert)
        {


            try
            {
                Authenticates.CreatePasswordHash(userInsert.Password, out byte[] passwordHash, out byte[] passwordSalt);
                userInsert.PasswordHash = passwordHash;
                userInsert.PasswordSalt = passwordSalt;

                var res = await _doctorRepositories.DoctorInsert(userInsert);

                return res.Status ? StatusCode(StatusCodes.Status201Created, res) : StatusCode(StatusCodes.Status409Conflict, res);
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }
        [HttpPost, Route("list")]
        public async Task<IActionResult> DoctorList([FromBody] JqueryDataTable userList)
        {
            try
            {
                var res = await _doctorRepositories.DoctorList(userList);
                return res.Status ? StatusCode(StatusCodes.Status200OK, res) : StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }

        [HttpPost, Route("update")]
        public async Task<IActionResult> DoctorUpdate([FromBody] DoctorUpdate userUpdate)
        {
            try
            {
                var responce = await _doctorRepositories.DoctorUpdate(userUpdate);

                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpGet, Route("get")]

        public async Task<IActionResult> DoctorGet(string DoctorId)
        {
            try
            {
                var responce = await _doctorRepositories.DoctorGet(DoctorId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpDelete, Route("delete")]
        public async Task<IActionResult> DoctorDelete(string DoctorId)
        {
            try
            {
                var responce = await _doctorRepositories.DoctorDelete(DoctorId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {

                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }

    }
}
