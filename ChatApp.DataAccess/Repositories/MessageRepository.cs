using System.Linq.Expressions;
using ChatApp.DataAccess.EF;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories;

public class MessageRepository : AbstractRepository, IMessageRepository
{
    private readonly DbSet<Message> _dbSet;
    
    public MessageRepository(ApplicationDbContext context)
        : base(context)
    {
        _dbSet = Context.Set<Message>();
    }

    public async Task<Guid> CreateAsync(Message entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        
        return entry.Entity.Id;
    }

    public async Task<IEnumerable<Message>> GetAllAsync()
    {
        return await _dbSet
            .Include(m => m.Chat)
            .Include(m => m.Sender)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Include(m => m.Chat)
            .Include(m => m.Sender)
            .OrderBy(m => m.SentAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Message>> GetAllAsync(Expression<Func<Message, bool>> predicate)
    {
        return await _dbSet
            .Include(m => m.Chat)
            .Include(m => m.Sender)
            .Where(predicate)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }

    public async Task<Message?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(m => m.Chat)
            .Include(m => m.Sender)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<bool> UpdateAsync(Message entity)
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

    public async Task<bool> DeleteAsync(Message entity)
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