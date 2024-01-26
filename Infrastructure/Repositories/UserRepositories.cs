
using Azure;
using Dapper;
using HospitalManagement.Core.Interface;
using HospitalManagement.Core.Model;
using HospotalManagement.Generic.Helper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public class UserRepositories : IUserInterface
    {
        private static string _con = string.Empty;
        private static IConfigurationRoot _iconfiguration;
        public UserRepositories()
        {
           
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json");
            _iconfiguration = builder.Build();
            _con = _iconfiguration["ConnectionStrings:DataAccessConnection"];
        }
        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_con);
            }
        }
        public async Task<Responce> UserInsert(UserInsert userInsert)
        {
            Responce response = new();

            using (IDbConnection db = Connection)
            {
                var result = await db.QueryAsync<Responce>("[dbo].[uspUserInsert]", new
                {
                    userInsert.FirstName,
                    userInsert.LastName,
                    userInsert.Email,
                    userInsert.ContactNo,
                    userInsert.Address,
                    userInsert.HospitalName,
                    userInsert.RoleID,
                    userInsert.ZipCode,
                    userInsert.Gender,
                    userInsert.PasswordHash,
                    userInsert.PasswordSalt,
                    userInsert.CreatedBy
                }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                response = result.FirstOrDefault();
            }
            return response;
        }
    }
}
