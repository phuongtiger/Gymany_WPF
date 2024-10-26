using DataAccess.DAOs;
using Model;
using Repository.Interface;

namespace Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private PaymentDAO paymentDAO;
        public PaymentRepository(PaymentDAO item)
        {
            paymentDAO = item;
        }

        public async Task<IEnumerable<Payment>> GetListAll() => await paymentDAO.GetListAll();
        public async Task<Payment> GetById(int id) => await paymentDAO.GetById(id);
        public async Task Add(Payment item) => await paymentDAO.Add(item);
        public async Task Update(Payment item) => await paymentDAO.Update(item);
        public async Task Delete(int id) => await paymentDAO.Delete(id);
    }
}
