using HospitalManagement.Core.Model;
using HospotalManagement.Generic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Core.Interface
{
    public interface IAppointmentRepositories
    {
        Task<Responce> AppointmentInsert(AppointmentInsert appointmentInsert);
        Task<ClsResponse<AppointmentListDestails>> AppointmentList(JqueryDataTable jqueryDataTable);
        Task<Responce> AppointmentUpdate(AppointmentUpdate appointmentUpdate);
        Task<ClsResponse<AppointmentListDestails>> AppointmentGet(string AppointmentId);
        Task<Responce> AppointmentDelete(string AppointmentId);
        Task<ClsResponse<AppointmentListDestails>> AppointmentListByRole(AppointmentList appointmentList);


    }
}
