using System;
using Model;

namespace Repository.Interface
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetListAll();
        Task<Admin> GetById(int id);
    }
}
