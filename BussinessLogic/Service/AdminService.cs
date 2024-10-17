using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.Interface;
using Model;
using Repository;
using Repository.Interface;

namespace BussinessLogic.Service
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<IEnumerable<Admin>> GetListAllAdmin() => await _adminRepository.GetListAll();
        public async Task<Admin> GetByIdAdmin(int id) => await _adminRepository.GetById(id);

    }
}