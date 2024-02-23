using HospitalManagement.Core.Model;
using HospotalManagement.Generic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Core.Interface
{
    public interface IPatientRepositories
    {
        Task<Responce> PatientInsert(PatientsInsert patientsInsert);
        Task<ClsResponse<PatientsList>> PatientList(JqueryDataTable jqueryDataTable);
        Task<Responce> PatientUpdate(PatinetsUpdate patinetsUpdate);
        Task<ClsResponse<PatientsList>> PatinetGet(string UserId);
        Task<Responce> PatientDelete(string UserId);
    }
}
