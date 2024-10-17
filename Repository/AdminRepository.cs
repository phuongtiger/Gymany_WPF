using System;
using System.Collections.Generic;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAOs;
using Model;
using Repository.Interface;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Repository
{
    public class AdminRepository : IAdminRepository
    {
        private AdminDAO adminDAO;
        public AdminRepository(AdminDAO item)
        {
            adminDAO = item;
        }

        public async Task<IEnumerable<Admin>> GetListAll() => await adminDAO.GetListAll();
        public async Task<Admin> GetById(int id) => await adminDAO.GetById(id);


    }
}
