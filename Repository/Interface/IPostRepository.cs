using Model;

namespace Repository.Interface
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetListAll();
        Task<Post> GetById(int id);
        Task Add(Post item);
        Task Update(Post item);
        Task Delete(int id);
        Task<IList<Post>> GetListPostByPt(int ptId);
    }
}
