using BussinessLogic.Interface;
using Model;
using Repository.Interface;

namespace BussinessLogic.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Post>> GetListAllPost() => await _repository.GetListAll();
        public async Task<Post> GetByIdPost(int id) => await _repository.GetById(id);
        public async Task AddPost(Post item) => await _repository.Add(item);
        public async Task UpdatePost(Post item) => await _repository.Update(item);
        public async Task DeletePost(int id) => await _repository.Delete(id);
    }
}
