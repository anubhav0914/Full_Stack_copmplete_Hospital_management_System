using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Hospital.Persistence.Repository
{
    public class HospitalRepository<T> : IHospitalRepository<T> where T : class
    {
        protected readonly HospitalDBContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public HospitalRepository(HospitalDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            return result;
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<bool> AddAsync(T dbRecord)
        {
            await _dbSet.AddAsync(dbRecord);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<T> GetById(int id)
        {
            var result = await _dbSet.FindAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(T dbRecord)
        {
            _dbSet.Update(dbRecord);
            var result = await _dbContext.SaveChangesAsync();
            if (result != null) return true;
            else return false;

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbSet.FindAsync(id);

            if (result == null) return false;
            _dbSet.Remove(result);
            return await _dbContext.SaveChangesAsync() > 0;

        }
    }
}


