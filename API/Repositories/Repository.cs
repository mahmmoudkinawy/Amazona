﻿namespace API.Repositories;
public sealed class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AmazonaDbContext _context;

    public Repository(AmazonaDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(
            new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<T>> GetEntitiesWithSpecAsync(
        ISpecification<T> specification, CancellationToken cancellationToken)
    {
        return await ApplySpecification(specification).ToListAsync(cancellationToken);
    }

    public async Task<T> GetEntityWithSpecAsync(
        ISpecification<T> specification, CancellationToken cancellationToken)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> GetCountWithSpecAsync(ISpecification<T> specification, 
        CancellationToken cancellationToken)
    {
        return await ApplySpecification(specification).CountAsync(cancellationToken);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);
    }

    public async Task Add(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
