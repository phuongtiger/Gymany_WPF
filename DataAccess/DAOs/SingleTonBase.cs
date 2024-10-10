
namespace DataAccess.DAOs
{
    public class SingleTonBase<T> where T : class, new()
    {
        private static T _instance;
        private static readonly object _lock = new object();
        public static GymanyDbsContext _context = new GymanyDbsContext();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();

                    }
                    return _instance;
                }
            }

        }
    }
}