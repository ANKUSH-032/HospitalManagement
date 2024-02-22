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
    public class DoctorRepositories : IDoctorRepositories
    {
        private static string _con = string.Empty;
        private static IConfigurationRoot? _iconfiguration;
        public DoctorRepositories()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json");
            _iconfiguration = builder.Build();
            _con = _iconfiguration["ConnectionStrings:DataAccessConnection"]!;
        }
        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_con);
            }
        }
        public async Task<Responce> DoctorInsert(DoctorInsert userInsert)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspDoctorInsert]", new
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

            Responce? response = result.FirstOrDefault();

            return response!;
        }

        public async Task<ClsResponse<DoctorList>> DoctorList(JqueryDataTable jqueryDataTable)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspDoctorGetList]", new
            {
                jqueryDataTable.Start,
                jqueryDataTable.SortCol,
                jqueryDataTable.PageSize,
                jqueryDataTable.SearchKey
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<DoctorList>? responce = result.Read<ClsResponse<DoctorList>>().FirstOrDefault();
            if (responce!.Status)
            {
                responce.Data = result.Read<DoctorList>().ToList();
                responce.TotalRecords = result.Read<int>().FirstOrDefault();
            }
            return responce;


        }

        public async Task<Responce> DoctorUpdate(DoctorUpdate userUpdate)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspDoctorUpdate]", new
            {
                userUpdate.FirstName,
                userUpdate.LastName,
                userUpdate.Email,
                userUpdate.Address,
                userUpdate.HospitalName,
                userUpdate.DoctorId,
                userUpdate.Gender,
                userUpdate.ContactNo,
                userUpdate.ZipCode,

            }, commandType: CommandType.StoredProcedure);
            Responce? responce = result.FirstOrDefault();

            return responce!;
        }

        public async Task<ClsResponse<Doctor>> DoctorGet(string DoctorId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspDoctorGetDetails]", new
            {
                DoctorId 
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<Doctor>? responce = result.Read<ClsResponse<Doctor>>().FirstOrDefault();

            if (responce!.Status)
            {
                responce.Data = result.Read<Doctor>().ToList();

            }
            return responce;

        }
        public async Task<Responce> DoctorDelete(string DoctorId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspDoctorDelete]", new
            {
                DoctorId
            }, commandType: CommandType.StoredProcedure);

            Responce? responce = result.FirstOrDefault();


            return responce!;

        }
    }
}
