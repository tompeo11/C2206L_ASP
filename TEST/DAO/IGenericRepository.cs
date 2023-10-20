using System.Linq.Expressions;

namespace TEST.DAO;

public interface IGenericRepository<T> where T : class
{
    IEnumerable<T> GetAll(string? includeProperties = null);
    public IEnumerable<T> GetEntities(Expression<Func<T, bool>>? filter, string? includeProperties = null,
        string? orderBy = null, bool isDescending = false);

    T? GetEntityById(object id);
    void Add(T obj);
    void Update(T obj);
    void Delete(object id);
}