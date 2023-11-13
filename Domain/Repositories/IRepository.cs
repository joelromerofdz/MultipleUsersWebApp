using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRepository<T, TResponse>
    {
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<IEnumerable<TResponse>> GetByAsync(string value);
        Task AddMultipleAsync(IEnumerable<T> entity);
        Task AddAsync(T entity);
    }
}