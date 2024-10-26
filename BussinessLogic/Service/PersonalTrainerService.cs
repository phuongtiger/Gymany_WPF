using BussinessLogic.Interface;
using Model;
using Repository.Interface;

namespace BussinessLogic.Service
{
    public class PersonalTrainerService : IPersonalTrainerService
    {
        private readonly IPersonalTrainerRepository _personalTrainerRepository;
        public PersonalTrainerService(IPersonalTrainerRepository personalTrainerRepository)
        {
            _personalTrainerRepository = personalTrainerRepository;
        }

        public async Task<IEnumerable<PersonalTrainer>> GetListAllPersonalTrainer() => await _personalTrainerRepository.GetListAll();
        public async Task<PersonalTrainer> GetByIdPersonalTrainer(int id) => await _personalTrainerRepository.GetById(id);
        public async Task AddPersonalTrainer(PersonalTrainer item) => await _personalTrainerRepository.Add(item);
        public async Task UpdatePersonalTrainer(PersonalTrainer item) => await _personalTrainerRepository.Update(item);
        public async Task DeletePersonalTrainer(int id) => await _personalTrainerRepository.Delete(id);

       
    }
}
