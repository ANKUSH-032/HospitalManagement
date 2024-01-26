using HospitalManagement.Core.Model;
using HospotalManagement.Generic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Core.Interface
{
    public interface IUserInterface
    {
        Task<Responce> UserInsert(UserInsert userInsert);
    }
}
