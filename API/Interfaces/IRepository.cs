namespace API.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> GetEntityWithSpecAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> GetEntitiesWithSpecAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<int> GetCountWithSpecAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task Add(T entity, CancellationToken cancellationToken);
    Task Remove(T entity, CancellationToken cancellationToken);
    Task Update(T entity, CancellationToken cancellationToken);
}
