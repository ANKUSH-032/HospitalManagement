using System;
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
}
