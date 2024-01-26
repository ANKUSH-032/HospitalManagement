using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospotalManagement.Generic.Helper
{
    public class Authenticates
    {
        private static readonly IConfigurationRoot _iconfiguration;
        private static readonly string? _con = string.Empty;
        private static readonly string? _secretKey = string.Empty;

        static Authenticates()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json");
            _iconfiguration = builder.Build();

            _con = _iconfiguration["ConnectionStrings:DataAccessConnection"];
            _secretKey = _iconfiguration["AppSettings:Secret"];
        }
        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_con);
            }

        }
        private static LoginUser Authentication<T>(AuthenticationRequest request)
        {
            LoginUser loginUser = new();

            dynamic response = GetUserDetails<T>(Email: request.Email);
            if (response == null)
            {
                return null;
            }
            else if (response.Name.ToUpper().Equals("USERNOTREGISTER") || response.Name.ToUpper().Equals("DELETED"))
            {
                return response;
            }
            else
            {
                if (!VerifyPasswordHash(request.Password, response.PasswordHash, response.PasswordSalt))
                    return null;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                          new Claim(ClaimTypes.Name , response.UserId ), new Claim(ClaimTypes.Role, response.Role), new Claim(ClaimTypes.Email, response.Email)
                    }
                    ),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                loginUser.UserId = response.UserId;
                loginUser.Email = response.Email;
                loginUser.Name = response.Name;
                loginUser.Token = tokenHandler.WriteToken(token);
                loginUser.Role = response.Role;

                return loginUser;

            }
        }
        public static dynamic Login<T>(AuthenticationRequest loginCredentials)
        {
            return Authentication<T>(loginCredentials);
        }
        public static dynamic GetUserDetails<T>(string? UserId = null, string? Email = null)
        {
            dynamic? response;

            using (IDbConnection db = Connection)
            {
                response = db.QueryFirstOrDefault<T>("[dbo].[uspUserDetailsGet]", new
                {
                    UserId,
                    Email
                }, commandType: CommandType.StoredProcedure);
            }

            return response;
        }
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            try
            {
                string storedSaltStr = Encoding.ASCII.GetString(storedSalt);
                var newPassword = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(password, storedSaltStr);
                string oldPassword = Encoding.Default.GetString(storedHash);
                return newPassword == oldPassword;
            }
            catch
            {
                throw;
            }
        }
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var mySalt = DevOne.Security.Cryptography.BCrypt.BCryptHelper.GenerateSalt();
            passwordSalt = Encoding.ASCII.GetBytes(mySalt);

            var myHash = DevOne.Security.Cryptography.BCrypt.BCryptHelper.HashPassword(password, mySalt);
            passwordHash = Encoding.ASCII.GetBytes(myHash);
        }
        

    }
}
