using TEST.Data;
using TEST.Models;

namespace TEST.DAO
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private GenericRepository<Category> _categoryRepository;
        private GenericRepository<CoverType> _coverTypeRepository;
        private GenericRepository<Product> _productRepository;

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

        public GenericRepository<CoverType> coverTypeRepository
        {
            get
            {
                if (_coverTypeRepository == null)
                {
                    this._coverTypeRepository = new GenericRepository<CoverType>(_db);
                }

                return _coverTypeRepository;
            }
        }

        public GenericRepository<Product> productRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    this._productRepository = new GenericRepository<Product>(_db);
                }

                return _productRepository;
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
