using System.Linq.Expressions;
using ChatApp.DataAccess.EF;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories;

public class ChatRepository : AbstractRepository, IChatRepository
{
    private readonly DbSet<Chat> _dbSet;
    
    public ChatRepository(ApplicationDbContext context) 
        : base(context)
    {
        _dbSet = Context.Set<Chat>();
    }

    public async Task<Guid> CreateAsync(Chat entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        
        return entry.Entity.Id;
    }

    public async Task<IEnumerable<Chat>> GetAllAsync()
    {
        return await _dbSet
            .Include(c => c.Messages)
            .ToListAsync();
    }

    public async Task<IEnumerable<Chat>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Include(c => c.Messages)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Chat>> GetAllAsync(Expression<Func<Chat, bool>> predicate)
    {
        return await _dbSet
            .Include(c => c.Messages)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<Chat?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> UpdateAsync(Chat entity)
    {
        try
        {
            _dbSet.Update(entity);
            var result = await Context.SaveChangesAsync();
            return result > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Chat entity)
    {
        try
        {
            _dbSet.Remove(entity);
            var result = await Context.SaveChangesAsync();
            return result > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;
                
            _dbSet.Remove(entity);
            var result = await Context.SaveChangesAsync();
            return result > 0;
        }
        catch
        {
            return false;
        }
    }
}