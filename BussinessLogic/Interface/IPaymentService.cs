using Model;

namespace BussinessLogic.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetListAllPayment();
        Task<Payment> GetByIdPayment(int id);
        Task AddPayment(Payment item);
        Task UpdatePayment(Payment item);
        Task DeletePayment(int id);
    }
}
