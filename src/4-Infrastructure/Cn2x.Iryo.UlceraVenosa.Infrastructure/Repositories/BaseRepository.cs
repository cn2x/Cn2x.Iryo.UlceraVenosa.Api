using Microsoft.EntityFrameworkCore;
using Cn2x.Iryo.UlceraVenosa.Domain.Interfaces;
using Cn2x.Iryo.UlceraVenosa.Infrastructure.Data;
using Cn2x.Iryo.UlceraVenosa.Domain.Core;

namespace Cn2x.Iryo.UlceraVenosa.Infrastructure.Repositories;

/// <summary>
/// Implementação base do repositório genérico
/// </summary>
/// <typeparam name="T">Tipo da entidade</typeparam>
public abstract class BaseRepository<T> : IRepository<T> where T : class, IAggregateRoot
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public IUnitOfWork UnitOfWork => _context;

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        var result = await _dbSet.AddAsync(entity);        
        return result.Entity;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);        
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _dbSet.Remove(entity);        
        return true;
    }

    public virtual async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().AnyAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public Task<IQueryable<M?>> FindFilterByExpression<M>(System.Linq.Expressions.Expression<System.Func<M, bool>> expression) where M : class, IEntity<System.Guid>
    {
        // Implementação básica para evitar erro de compilação
        return Task.FromResult(_context.Set<M>().Where(expression).AsNoTracking().AsQueryable() as IQueryable<M?>);
    }
} 