using Azure;
using HospotalManagement.Generic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Core.Interface
{
    public interface ICommonRepository
    {

        Task<ClsResponse<MasterList>> MasterListAsync(string type);

    }
}
