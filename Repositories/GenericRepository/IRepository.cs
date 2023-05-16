namespace DUCtrongAPI.Repositories.GenericRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(string id);     
        Task<bool> Insert(T entity);        
        Task<bool> Update();
        Task<bool> Remove(T entity);
        
    }
}
