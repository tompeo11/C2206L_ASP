using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TEST.Data;

namespace TEST.DAO
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet =  _db.Set<T>();
        }

        public void Add (T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Update (T entity)
        {
            _db.ChangeTracker.Clear();
            dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public T GetEntityById(object id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) 
        {
            IQueryable<T> query = dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }
    }
}
