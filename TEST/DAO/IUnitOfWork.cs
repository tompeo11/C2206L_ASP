using TEST.Models;

namespace TEST.DAO
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        GenericRepository<Category> categoryRepository { get; }
        GenericRepository<CoverType> coverTypeRepository { get; }
        GenericRepository<Product> productRepository { get; }
    }
}
