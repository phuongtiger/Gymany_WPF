using Model;
using DataAccess.DAOs;
using Repository.Interface;

namespace Repository
{
    public class PostRepository : IPostRepository
    {
        private PostDAO DAO;
        public PostRepository(PostDAO item)
        {
            DAO = item;
        }

        public async Task<IEnumerable<Post>> GetListAll() => await DAO.GetListAll();
        public async Task<Post> GetById(int id) => await DAO.GetById(id);
        public async Task Add(Post item) => await DAO.Add(item);
        public async Task Update(Post item) => await DAO.Update(item);
        public async Task Delete(int id) => await DAO.Delete(id);
    }
}
