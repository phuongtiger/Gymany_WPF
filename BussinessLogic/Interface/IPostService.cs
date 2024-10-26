using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BussinessLogic.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetListAllPost();
        Task<Post> GetByIdPost(int id);
        Task AddPost(Post item);
        Task UpdatePost(Post item);
        Task DeletePost(int id);
    }
}
