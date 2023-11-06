using TEST.Data;
using TEST.Models;

namespace TEST.DAO
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        private GenericRepository<Vaccine> _vaccineRepository;
        private GenericRepository<VaccineType> _vaccineTypeRepository;
        private GenericRepository<VaccineSchedule> _vaccineScheduleRepository;

        public UnitOfWork(ApplicationDbContext dbContext) 
        {
            _db = dbContext;
        }

        public GenericRepository<Vaccine> vaccineRepository
        {
            get
            {
                if (_vaccineRepository == null)
                {
                    this._vaccineRepository = new GenericRepository<Vaccine>(_db);
                }

                return _vaccineRepository;
            }
        }

        public GenericRepository<VaccineType> vaccineTypeRepository
        {
            get
            {
                if (_vaccineTypeRepository == null)
                {
                    this._vaccineTypeRepository = new GenericRepository<VaccineType>(_db);
                }

                return _vaccineTypeRepository;
            }
        }

        public GenericRepository<VaccineSchedule> vaccineScheduleRepository
        {
            get
            {
                if (_vaccineScheduleRepository == null)
                {
                    this._vaccineScheduleRepository = new GenericRepository<VaccineSchedule>(_db);
                }

                return _vaccineScheduleRepository;
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
