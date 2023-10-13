using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TEST.Data;

namespace TEST.DAO
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        T GetEntityById(object id);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
    }
}
