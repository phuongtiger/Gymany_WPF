using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAOs;
using Model;
using Repository.Interface;

namespace Repository
{
    public class PersonalTrainerRepository : IPersonalTrainerRepository
    {
        private PersonalTrainerDAO personalTrainerDAO;
        public PersonalTrainerRepository(PersonalTrainerDAO item)
        {
            personalTrainerDAO = item;
        }

        public async Task<IEnumerable<PersonalTrainer>> GetListAll() => await personalTrainerDAO.GetListAll();
        public async Task<PersonalTrainer> GetById(int id) => await personalTrainerDAO.GetById(id);
        public async Task Add(PersonalTrainer item) => await personalTrainerDAO.Add(item);
        public async Task Update(PersonalTrainer item) => await personalTrainerDAO.Update(item);
        public async Task Delete(int id) => await personalTrainerDAO.Delete(id);
        public async Task<PersonalTrainer> GetByUsername(string username) => await personalTrainerDAO.GetByUsername(username);

    }
}
