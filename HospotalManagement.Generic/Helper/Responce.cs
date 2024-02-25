﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospotalManagement.Generic.Helper
{
    public class Responce
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? Data { get; set; }
    }

    public class ClsResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? Data { get; set; }
    }
    public class ClsResponse<T>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public long? TotalRecords { get; set; } = 0;
        public long RecordsFiltered { get; set; }
    }
    public class JqueryDataTable
    {
        public string? SearchKey { get; set; } //= string.Empty;
        public int? Start { get; set; }
        public int? PageSize { get; set; }
        public string? SortCol { get; set; }// = string.Empty;
    }

    public class AppointmentList
    {
        public string? SearchKey { get; set; } //= string.Empty;
        public int? Start { get; set; }
        public int? PageSize { get; set; }
        public string? SortCol { get; set; }// = string.Empty;
        public string? DoctorId { get; set; }
        public string? PatientId { get; set; }
    }
}
