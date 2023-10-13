using TEST.Data;
using TEST.Models;

namespace TEST.DAO
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        private GenericRepository<Category> _categoryRepository;

        public UnitOfWork(ApplicationDbContext dbContext) 
        {
            _db = dbContext;
        }

        public GenericRepository<Category> categoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    this._categoryRepository = new GenericRepository<Category>(_db);
                }

                return _categoryRepository;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
