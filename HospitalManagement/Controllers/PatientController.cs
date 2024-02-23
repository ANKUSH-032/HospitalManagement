using HospitalManagement.Core.Interface;
using HospitalManagement.Core.Model;
using HospotalManagement.Generic.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPatientRepositories _patinetRepositories;
        public PatientController(IPatientRepositories patinetRepositories, IConfiguration configuration)
        {
            _patinetRepositories = patinetRepositories;
            _configuration = configuration;

        }
        [HttpPost, Route("insert")]
        public async Task<IActionResult> PatientInsert([FromBody] PatientsInsert patientsInsert)
        {


            try
            {
                Authenticates.CreatePasswordHash(patientsInsert.Password, out byte[] passwordHash, out byte[] passwordSalt);
                patientsInsert.PasswordHash = passwordHash;
                patientsInsert.PasswordSalt = passwordSalt;           

                var res = await _patinetRepositories.PatientInsert(patientsInsert);

                return res.Status ? StatusCode(StatusCodes.Status201Created, res) : StatusCode(StatusCodes.Status409Conflict, res);
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }
        [HttpPost, Route("list")]
        public async Task<IActionResult> PatientList([FromBody] JqueryDataTable patinetList)
        {
            try
            {
                var res = await _patinetRepositories.PatientList(patinetList);
                return res.Status ? StatusCode(StatusCodes.Status200OK, res) : StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }

        [HttpPost, Route("update")]
        public async Task<IActionResult> PatientUpdate([FromBody] PatinetsUpdate patinetsUpdate)
        {
            try
            {
                var responce = await _patinetRepositories.PatientUpdate(patinetsUpdate);

                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpGet, Route("get")]

        public async Task<IActionResult> PatinetGet(string patientId)
        {
            try
            {
                var responce = await _patinetRepositories.PatinetGet(patientId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpDelete, Route("delete")]
        public async Task<IActionResult> PatientDelete(string patinetId)
        {
            try
            {
                var responce = await _patinetRepositories.PatientDelete(patinetId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {

                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }

    }
}
