using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository.Interface
{
    public interface IPersonalTrainerRepository
    {
        Task<IEnumerable<PersonalTrainer>> GetListAll();
        Task<PersonalTrainer> GetById(int id);
        Task Add(PersonalTrainer item);
        Task Update(PersonalTrainer item);
        Task Delete(int id);
    }
}
