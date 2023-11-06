using TEST.Models;

namespace TEST.DAO
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        GenericRepository<Vaccine> vaccineRepository { get; }
        GenericRepository<VaccineType> vaccineTypeRepository { get; }
        GenericRepository<VaccineSchedule> vaccineScheduleRepository { get; }
    }
}
