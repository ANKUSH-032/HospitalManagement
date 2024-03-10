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
    public class CommonRepository : ICommonRepository
    {
        private static string _con = string.Empty;
        private static IConfigurationRoot? _iconfiguration;
        public CommonRepository()
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

        public async Task<ClsResponse<MasterList>> MasterListAsync(string type)
        {
          
            using (IDbConnection db = Connection)
            {
               
                var result = await db.QueryMultipleAsync("[dbo].[uspMasterList]", new
                {
                    Type = type
                     
                }, commandType: CommandType.StoredProcedure);

                ClsResponse<MasterList> responce = result.Read<ClsResponse<MasterList>>().FirstOrDefault();

                if (responce!.Status)
                {
                    responce.Data = result.Read<MasterList>().ToList();

                }
                return responce;
            }

           
        }

    }
}
