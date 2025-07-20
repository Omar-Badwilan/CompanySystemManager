using CompanySystem.DataAccessLayer.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.DataAccessLayer.Persistence.Repositories._Generic
{
    public class GenericRepository<T>(ApplicationDbContext dbContext) where T : BaseEntity
    {
        private protected readonly ApplicationDbContext _dbContext = dbContext;


        //CRUD Operations
        public IEnumerable<T> GetAll(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return _dbContext.Set<T>().AsNoTracking().ToList();

            return _dbContext.Set<T>().ToList();
        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>();
        }
        public T? GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public int Add(T T)
        {
            _dbContext.Set<T>().Add(T);
            return _dbContext.SaveChanges();
        }
        public int Update(T T)
        {
            _dbContext.Set<T>().Update(T);
            return _dbContext.SaveChanges();
        }
        public int Delete(T T)
        {
            _dbContext.Set<T>().Remove(T);
            return _dbContext.SaveChanges();
        }
    }
}
