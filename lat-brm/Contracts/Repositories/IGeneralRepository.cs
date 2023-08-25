namespace lat_brm.Contracts.Repositories
{
    public interface IGeneralRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetByGuid(Guid id);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
