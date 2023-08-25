using lat_brm.Contracts.Repositories;
using lat_brm.Data;
using lat_brm.Models;

namespace lat_brm.Repositories
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        private readonly EmployeeDbContext _context;

        public GeneralRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public TEntity Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public TEntity? GetByGuid(Guid id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            _context.ChangeTracker.Clear();
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
    }
}
