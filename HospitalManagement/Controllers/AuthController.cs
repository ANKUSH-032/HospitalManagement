using HospotalManagement.Generic.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;

namespace HospitalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticationRequest authenticationRequest)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(
                        new
                        {
                            Status = false,
                            Message = string.Join(Environment.NewLine, ModelState.Values
                               .SelectMany(x => x.Errors)
                               .Select(x => x.ErrorMessage))
                        });
                }
                authenticationRequest.Email = authenticationRequest.Email.ToLower();
                LoginUser user = Authenticates.Login<LoginUser>(authenticationRequest);

                if (user == null)
                {
                    ClsResponse rep = new()
                    {

                        Message = MessageHelper.invalidCredentials,
                        Data = null
                    };
                    return StatusCode(StatusCodes.Status401Unauthorized, rep);

                }

                else
                {
                    ClsResponse rep = new();
                    if (user.Name.ToUpper(CultureInfo.CurrentCulture).Equals("DELETED"))
                    {
                        rep.Message = MessageHelper.userNotFoundForEmail;
                        rep.Data = null;
                        return StatusCode(StatusCodes.Status403Forbidden, rep);
                    }


                    if (!string.IsNullOrWhiteSpace(user.Name) && user.Name.ToUpper(CultureInfo.CurrentCulture).Equals("USERNOTREGISTER"))
                    {
                        rep.Message = MessageHelper.userNotRegister;
                        rep.Data = null;
                        return StatusCode(StatusCodes.Status403Forbidden, rep);
                    }

                }

                return Ok(new { Status = true, Message = "Success", Userdetails = user });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }

        }
        [HttpGet, Route("logout")]
        public IActionResult Logout()
        {
            try
            {
                var tokenstring = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                ClsResponse rep = new()
                {
                    Data = null
                };
                if (string.IsNullOrWhiteSpace(tokenstring))
                {
                    rep.Message = "Token is required for logging out .";
                    return StatusCode(StatusCodes.Status401Unauthorized, rep);
                }

                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken;
                try
                {
                    jwtToken = handler.ReadToken(tokenstring.Split(" ")[1]) as JwtSecurityToken;
                }
                catch (Exception)
                {
                    rep.Message = "Token is not well formed.";
                    return StatusCode(StatusCodes.Status401Unauthorized, rep);
                }

                if (jwtToken?.Claims?.FirstOrDefault() == null || string.IsNullOrWhiteSpace(jwtToken.Claims.FirstOrDefault().Value))
                {
                    rep.Message = "Token is invalid.";
                    return StatusCode(StatusCodes.Status401Unauthorized, rep);
                }

                string CurrentLoggedInUserEmail = jwtToken.Claims.Skip(2).FirstOrDefault().Value;

                return Ok(new
                {
                    Message = string.Format(CultureInfo.CurrentCulture, "User successfully logged out."),
                    Status = true
                });
            }
            catch (Exception ex)
            {
                //Logger.AddErrorLog(ControllerContext.ActionDescriptor.ControllerName, "Logout", User.Identity.Name, ex);
                return StatusCode(StatusCodes.Status500InternalServerError, MessageHelper.message);
            }
        }
    }
}


