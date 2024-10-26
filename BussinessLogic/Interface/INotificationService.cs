using Model;

namespace BussinessLogic.Interface
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetListAllNotification();
        Task<Notification> GetByIdNotification(int id);
        Task AddNotification(Notification item);
        Task UpdateNotification(Notification item);
        Task DeleteNotification(int id);
    }
}
