using CompanySystem.DataAccessLayer.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.DataAccessLayer.Persistence.Repositories._Generic
{
    public class GenericRepository<T>(ApplicationDbContext dbContext) : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly ApplicationDbContext _dbContext = dbContext;


        //CRUD Operations
        public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return await _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();
        }

        public IQueryable<T> GetIQueryable()
        {
            return _dbContext.Set<T>();
        }

        public IEnumerable<T> GetIEnumerable()
        {
            return _dbContext.Set<T>();
        }


        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }



        public void Add(T entity) => _dbContext.Set<T>().Add(entity);

        public void Update(T entity) => _dbContext.Set<T>().Update(entity);
        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
        }


    }
}
