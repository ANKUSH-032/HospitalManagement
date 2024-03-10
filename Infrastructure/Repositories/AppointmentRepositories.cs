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
using AppointmentList = HospotalManagement.Generic.Helper.AppointmentList;


namespace Infrastructure.Repositories
{
    public class AppointmentRepositories : IAppointmentRepositories
    {
        private static string _con = string.Empty;
        private static IConfigurationRoot? _iconfiguration;
        public AppointmentRepositories()
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
        public async Task<Responce> AppointmentInsert(AppointmentInsert appointmentInsert)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspAppointmentInsert]", new
            {
                appointmentInsert.DoctorId,
                appointmentInsert.PatientId,
                appointmentInsert.AppointmentDescription,
                appointmentInsert.AppointmentType,
                appointmentInsert.AppointmentDate,
                appointmentInsert.AppointmentTime,
                appointmentInsert.AppointmentTitle,
                
            }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

            Responce? response = result.FirstOrDefault();

            return response!;
        }

        public async Task<ClsResponse<AppointmentListDestails>> AppointmentList(JqueryDataTable jqueryDataTable)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspAppointmentGetList]", new
            {
                jqueryDataTable.Start,
                jqueryDataTable.SortCol,
                jqueryDataTable.PageSize,
                jqueryDataTable.SearchKey
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<AppointmentListDestails>? responce = result.Read<ClsResponse<AppointmentListDestails>>().FirstOrDefault();
            if (responce!.Status)
            {
                responce.Data = result.Read<AppointmentListDestails>().ToList();
                //responce.TotalRecords = result.Read<int>().FirstOrDefault();
            }
            return responce;


        }

        public async Task<Responce> AppointmentUpdate(AppointmentUpdate appointmentUpdate)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspAppointmentUpdate]", new
            {
                appointmentUpdate.DoctorId,
                appointmentUpdate.PatientId,
                appointmentUpdate.AppointmentDescription,
                appointmentUpdate.AppointmentType,
                appointmentUpdate.AppointmentDate,
                appointmentUpdate.AppointmentTime,
                appointmentUpdate.AppointmentTitle,
                appointmentUpdate.AppointmentId

            }, commandType: CommandType.StoredProcedure);
            Responce? responce = result.FirstOrDefault();

            return responce!;
        }

        public async Task<ClsResponse<AppointmentListDestails>> AppointmentGet(string AppointmentId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspAppointmentGetDetails]", new
            {
                AppointmentId
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<AppointmentListDestails>? responce = result.Read<ClsResponse<AppointmentListDestails>>().FirstOrDefault();

            if (responce!.Status)
            {
                responce.Data = result.Read<AppointmentListDestails>().ToList();

            }
            return responce;

        }
        public async Task<Responce> AppointmentDelete(string AppointmentId)
        {
            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspAppointmentDelete]", new
            {
                AppointmentId
            }, commandType: CommandType.StoredProcedure);

            Responce? responce = result.FirstOrDefault();   


            return responce!;

        }

        public async Task<ClsResponse<AppointmentListDestails>> AppointmentListByRole(AppointmentList appointmentList)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspAppointmentGetListByRole]", new
            {
                appointmentList.Start,
                appointmentList.SortCol,
                appointmentList.PageSize,
                appointmentList.SearchKey,
                appointmentList.PatientId,
                appointmentList.DoctorId
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<AppointmentListDestails>? responce = result.Read<ClsResponse<AppointmentListDestails>>().FirstOrDefault();
            if (responce!.Status)
            {
                responce.Data = result.Read<AppointmentListDestails>().ToList();
                responce.TotalRecords = result.Read<int>().FirstOrDefault();
            }
            return responce;


        }

        public async Task<ClsResponse<AppointmentAdminListDestails>> AppointmentListForAdmin(JqueryDataTable jqueryDataTable)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryMultipleAsync("[dbo].[uspAppointmentListForAdmin]", new
            {
                jqueryDataTable.Start,
                jqueryDataTable.SortCol,
                jqueryDataTable.PageSize,
                jqueryDataTable.SearchKey
            }, commandType: CommandType.StoredProcedure);

            ClsResponse<AppointmentAdminListDestails>? responce = result.Read<ClsResponse<AppointmentAdminListDestails>>().FirstOrDefault();
            if (responce!.Status)
            {
                responce.Data = result.Read<AppointmentAdminListDestails>().ToList();
                responce.TotalRecords = result.Read<int>().FirstOrDefault();
            }
            return responce;


        }

        public async Task<Responce> AppointmentSatusUpdate(AppointmentAppointmentUpdate appointmentAppointmentUpdate)
        {

            using IDbConnection? db = Connection;

            var result = await db.QueryAsync<Responce>("[dbo].[uspAppointmentSatusUpdate]", new
            {
                appointmentAppointmentUpdate.AppointmentId


            }, commandType: CommandType.StoredProcedure);
            Responce? responce = result.FirstOrDefault();

            return responce!;
        }
    }
}
