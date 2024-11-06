using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BussinessLogic.Interface
{
    public interface IPersonalTrainerService
    {
        Task<IEnumerable<PersonalTrainer>> GetListAllPersonalTrainer();
        Task<PersonalTrainer> GetByIdPersonalTrainer(int id);
        Task AddPersonalTrainer(PersonalTrainer item);
        Task UpdatePersonalTrainer(PersonalTrainer item);
        Task DeletePersonalTrainer(int id);
        Task<PersonalTrainer> GetByUsernamePersonalTrainer(string username);
    }
}
