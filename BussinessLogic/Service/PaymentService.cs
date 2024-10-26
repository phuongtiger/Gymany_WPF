using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.Interface;
using Model;
using Repository.Interface;

namespace BussinessLogic.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Payment>> GetListAllPayment() => await _repository.GetListAll();
        public async Task<Payment> GetByIdPayment(int id) => await _repository.GetById(id);
        public async Task AddPayment(Payment item) => await _repository.Add(item);
        public async Task UpdatePayment(Payment item) => await _repository.Update(item);
        public async Task DeletePayment(int id) => await _repository.Delete(id);
    }
}
