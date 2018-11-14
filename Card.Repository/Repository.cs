using Card.Infrastructure;
using Card.Interface.IRepo;
using Card.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardContext = Card.Model.CardContext;

namespace Card.Repository
{
   public class Repository<T> : IRepository<T> where T : class
    {
        private CardContext context;
        //private IDbContextTransaction dbContextTransaction;
        //private DbSet<T> entities;
        string errorMessage = string.Empty;

        public virtual IQueryable<T> Table
        {
            get
            {
                return context.Set<T>();
            }
        }

        public Repository(CardContext context)
        {
            this.context = context;
        }
        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> Get(object id)
        {
            return await context.Set<T>().FindAsync(id);
        }
        public async Task Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task Delete(object id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<int> GetCount()
        {
            var items = await context.Set<T>().ToListAsync();
            return items.Count;
        }

        public IEnumerable<T> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return context.Database.SqlQuery<T>(query, parameters);
        }
        public IEnumerable<T> ExecWithStoreProcedure(string querys)
        {
            return context.Database.SqlQuery<T>(querys);
        }
    }
}
