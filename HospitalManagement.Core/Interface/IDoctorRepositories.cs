using HospitalManagement.Core.Model;
using HospotalManagement.Generic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Core.Interface
{
    public interface IDoctorRepositories
    {
        Task<Responce> DoctorInsert(DoctorInsert userInsert);
        Task<ClsResponse<DoctorList>> DoctorList(JqueryDataTable jqueryDataTable);
        Task<Responce> DoctorUpdate(DoctorUpdate userUpdate);
        Task<ClsResponse<Doctor>> DoctorGet(string DoctorId);
        Task<Responce> DoctorDelete(string DoctorId);

    }
}
