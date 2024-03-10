using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Core.Model
{
    public class Appointment
    {
        [Required]
        public string? AppointmentTitle { get; set; }
        public string? AppointmentDescription { get; set; }
        [Required]
        public string? AppointmentType { get; set; }
        [Required]
        public string? AppointmentDate { get; set; }
        [Required]
        public string? AppointmentTime { get; set; }
        public string? DoctorId { get; set; }
        public string? PatientId { get; set; }

    }
    public class AppointmentUpdate : Appointment
    {
        public string? AppointmentId { get; set; }

    }
    public class AppointmentInsert : Appointment
    {
        public string? AppointmentId { get; set; }


    }
    public class AppointmentListDestails
    {
        public string? AppointmentId { get; set; }
        public string? AppointmentTitle { get; set; }
        public string? AppointmentDescription { get; set; }
        public string? AppointmentType { get; set; }
        public string? AppointmentDate { get; set; }
        public string? AppointmentTime { get; set; }
        public string? DoctorId { get; set; }
        public string? PatientId { get; set; }
        public string? PatinetName { get; set; }
        public string? Status { get; set; }
    }


    public class AppointmentAdminListDestails
    {
        public string? AppointmentId { get; set; }
        public string? AppointmentTitle { get; set; }
        public string? AppointmentDescription { get; set; }
        public string? AppointmentType { get; set; }
        public string? AppointmentDate { get; set; }
        public string? AppointmentTime { get; set; }
        public string? DoctorId { get; set; }
        public string? PatientId { get; set; }

        public string? Status { get; set; }
        public string? DoctorName { get; set; }
        public string? PatinetName { get; set; }

    }

    public class AppointmentAppointmentUpdate
    {
        public string? AppointmentId { get; set; }
        
    }
}
