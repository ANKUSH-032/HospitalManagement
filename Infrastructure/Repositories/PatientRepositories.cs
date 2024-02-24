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
    public class PatientRepositories : IPatientRepositories
    {
        private static string _con = string.Empty;
        private static IConfigurationRoot? _iconfiguration;
        public PatientRepositories()
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
        public async Task<Responce> PatientInsert(PatientsInsert patientsInsert)
        {

            using IDbConnection? db = Connection;

                var result = await db.QueryAsync<Responce>("[dbo].[uspPatientInsert]", new
            {
                patientsInsert.FirstName,
                patientsInsert.LastName,
                patientsInsert.Email,
                patientsInsert.ContactNo,
                patientsInsert.Address,
                patientsInsert.HospitalName,
                RoleID="patient",
                patientsInsert.ZipCode,
                patientsInsert.Gender,
                patientsInsert.PasswordHash,
                patientsInsert.PasswordSalt,
                patientsInsert.CreatedBy
            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            Responce? response = result.FirstOrDefault();

            return response!;
        }

        public async Task<ClsResponse<PatientsList>> PatientList(JqueryDataTable jqueryDataTable)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspPatientGetList]", new
            {
                jqueryDataTable.Start,
                jqueryDataTable.SortCol,
                jqueryDataTable.PageSize,
                jqueryDataTable.SearchKey
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<PatientsList>? responce = result.Read<ClsResponse<PatientsList>>().FirstOrDefault();
            if (responce!.Status)
            {
                responce.Data = result.Read<PatientsList>().ToList();
                responce.TotalRecords = result.Read<int>().FirstOrDefault();
            }
            return responce;


        }

        public async Task<Responce> PatientUpdate(PatinetsUpdate patinetsUpdate)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspPatinetUpdate]", new
            {
                patinetsUpdate.LastName,
                patinetsUpdate.FirstName,
                patinetsUpdate.Email,
                patinetsUpdate.Address,
                patinetsUpdate.HospitalName,
                patinetsUpdate. PatientId,
                patinetsUpdate.Gender,
                patinetsUpdate.ContactNo,
                patinetsUpdate.ZipCode,
                patinetsUpdate.UpdatedBy

            }, commandType: CommandType.StoredProcedure);
            Responce? responce = result.FirstOrDefault();

            return responce!;
        }

        public async Task<ClsResponse<PatientsList>> PatinetGet(string PatientId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspPatientGetDetails]", new
            {
                PatientId
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<PatientsList>? responce = result.Read<ClsResponse<PatientsList>>().FirstOrDefault();

            if (responce!.Status)
            {
                responce.Data = result.Read<PatientsList>().ToList();

            }
            return responce;

        }
        public async Task<Responce> PatientDelete(string PatientId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspPatentDelete]", new
            {
                PatientId
            }, commandType: CommandType.StoredProcedure);

            Responce? responce = result.FirstOrDefault();


            return responce!;

        }

    }
}
