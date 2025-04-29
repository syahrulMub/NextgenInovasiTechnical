namespace NextgenInovasiTest.BusinessLogic;

public interface IBusinessLogic<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false);
    Task<TEntity> GetByIdAsync(int id, bool includeDeleted = false);
    Task<TEntity> AddAsync(TEntity entity, string userId);
    Task<TEntity> UpdateAsync(TEntity entity, string userId);
    Task<int> SoftDeleteAsync(int id);
    Task HardDeleteAsync(int id);
    Task AddBacth(List<TEntity> entities, string userId);
}
