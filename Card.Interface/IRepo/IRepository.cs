using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Interface.IRepo
{
   public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(object id);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        IQueryable<T> Table { get; }

        IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters);
        IEnumerable<T> ExecWithStoreProcedure(string querys);
    }
}
