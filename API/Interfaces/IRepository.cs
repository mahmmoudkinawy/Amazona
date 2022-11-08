namespace API.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T> GetEntityWithSpecAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<IReadOnlyList<T>> GetEntitiesWithSpecAsync(ISpecification<T> specification, CancellationToken cancellationToken);
}
