using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Hospital.Persistence.Repository
{
    public interface IHospitalRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AddAsync(T dbRecord);

        Task<T> GetById(int id);

        Task<bool> UpdateAsync(T dbRecord);
        Task<bool> DeleteAsync(int id);
    }
}