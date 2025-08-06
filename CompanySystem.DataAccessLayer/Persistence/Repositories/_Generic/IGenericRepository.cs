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
        IEnumerable<T> GetAll(bool withAsNoTracking = true);
        IQueryable<T> GetIQueryable();
        IEnumerable<T> GetIEnumerable();

        T? GetById(int id);

        int Add(T entity);

        int Update(T entity);

        int Delete(T entity);
    }
}
