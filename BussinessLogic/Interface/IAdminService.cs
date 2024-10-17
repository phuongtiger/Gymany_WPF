using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BussinessLogic.Interface
{
    public interface IAdminService
    {
        Task<IEnumerable<Admin>> GetListAllAdmin();
        Task<Admin> GetByIdAdmin(int id);
    }
}
