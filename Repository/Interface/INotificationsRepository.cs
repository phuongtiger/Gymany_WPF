using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interface
{
    public interface INotificationsRepository
    {
        Task<IEnumerable<Notification>> GetListAll();
        Task<Notification> GetById(int id);
        Task Add(Notification item);
        Task Update(Notification item);
        Task Delete(int id);
        Task<IList<Notification>> GetListNotificationByPt(int ptId);
    }
}
