using CompanySystem.DataAccessLayer.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.DataAccessLayer.Persistence.Repositories._Generic
{
    public interface IGenericRepository <T> where T: BaseEntity
    {

        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true);
        IQueryable<T> GetIQueryable();
        IEnumerable<T> GetIEnumerable();
 

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
