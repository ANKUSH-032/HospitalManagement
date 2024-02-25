using HospitalManagement.Core.Interface;
using HospitalManagement.Core.Model;
using HospotalManagement.Generic.Helper;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppointmentList = HospotalManagement.Generic.Helper.AppointmentList;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepositories _appointmentRepositories;
        private readonly IConfiguration _configuration;
        public AppointmentController(IAppointmentRepositories appointmentRepositories, IConfiguration configuration)
        {
            _appointmentRepositories = appointmentRepositories;
            _configuration = configuration;

        }
        [HttpPost, Route("insert")]
        public async Task<IActionResult> AppointmentInsert([FromBody] AppointmentInsert appointmentInsert)
        {


            try
            {
              

                var res = await _appointmentRepositories.AppointmentInsert(appointmentInsert);

                return res.Status ? StatusCode(StatusCodes.Status201Created, res) : StatusCode(StatusCodes.Status409Conflict, res);
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }
        [HttpPost, Route("list")]
        public async Task<IActionResult> AppointmentList([FromBody] JqueryDataTable jqueryDataTable)
        {
            try
            {
                var res = await _appointmentRepositories.AppointmentList(jqueryDataTable);
                return res.Status ? StatusCode(StatusCodes.Status200OK, res) : StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }

        [HttpPost, Route("update")]
        public async Task<IActionResult> AppointmentUpdate([FromBody] AppointmentUpdate appointmentUpdate)
        {
            try
            {
                var responce = await _appointmentRepositories.AppointmentUpdate(appointmentUpdate);

                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpGet, Route("get")]

        public async Task<IActionResult> AppointmentGet(string AppointmentId)
        {
            try
            {
                var responce = await _appointmentRepositories.AppointmentGet(AppointmentId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpDelete, Route("delete")]
        public async Task<IActionResult> AppointmentDelete(string AppointmentId)
        {
            try
            {
                var responce = await _appointmentRepositories.AppointmentDelete(AppointmentId);
                return responce.Status ? StatusCode(StatusCodes.Status200OK, responce) : StatusCode(StatusCodes.Status400BadRequest, responce);
            }
            catch
            {

                return StatusCode(StatusCodes.Status400BadRequest, MessageHelper.message);
            }
        }
        [HttpPost, Route("appointment/list")]
        public async Task<IActionResult> AppointmentListByRole([FromBody] AppointmentList appointmentList)
        {
            try
            {
                var res = await _appointmentRepositories.AppointmentListByRole(appointmentList);
                return res.Status ? StatusCode(StatusCodes.Status200OK, res) : StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }

    }
}
