using System.Linq.Expressions;
using ChatApp.DataAccess.EF;
using ChatApp.DataAccess.Entities;
using ChatApp.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.Repositories;

public class UserRepository : AbstractRepository, IUserRepository
{
    private readonly DbSet<User> _dbSet;
    
    public UserRepository(ApplicationDbContext context)
        : base(context)
    {
        _dbSet = Context.Set<User>();
    }

    public async Task<Guid> CreateAsync(User entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        
        return entry.Entity.Id;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync(int pageNumber, int pageSize)
    {
        return await _dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(user => user.Chats)
            .FirstOrDefaultAsync(user => user.Id.Equals(id));
    }

    public async Task<bool> UpdateAsync(User entity)
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

    public async Task<bool> DeleteAsync(User entity)
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