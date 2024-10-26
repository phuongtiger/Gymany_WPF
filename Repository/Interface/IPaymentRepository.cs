using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interface
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetListAll();
        Task<Payment> GetById(int id);
        Task Add(Payment item);
        Task Update(Payment item);
        Task Delete(int id);
    }
}
