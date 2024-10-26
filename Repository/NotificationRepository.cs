using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAOs;
using Repository.Interface;
using Model;

namespace Repository
{
    public class NotificationRepository : INotificationsRepository
    {
        private NotificationDAO DAO;
        public NotificationRepository(NotificationDAO item)
        {
            DAO = item;
        }

        public async Task<IEnumerable<Notification>> GetListAll() => await DAO.GetListAll();
        public async Task<Notification> GetById(int id) => await DAO.GetById(id);
        public async Task Add(Notification item) => await DAO.Add(item);
        public async Task Update(Notification item) => await DAO.Update(item);
        public async Task Delete(int id) => await DAO.Delete(id);
    }
}
