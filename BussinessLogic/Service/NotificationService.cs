using BussinessLogic.Interface;
using Model;
using Repository.Interface;
namespace BussinessLogic.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationsRepository _repository;
        public NotificationService(INotificationsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Notification>> GetListAllNotification() => await _repository.GetListAll();
        public async Task<Notification> GetByIdNotification(int id) => await _repository.GetById(id);
        public async Task AddNotification(Notification item) => await _repository.Add(item);
        public async Task UpdateNotification(Notification item) => await _repository.Update(item);
        public async Task DeleteNotification(int id) => await _repository.Delete(id);
    }
}
