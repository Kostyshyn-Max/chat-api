using ChatApp.DataAccess.EF;

namespace ChatApp.DataAccess.Repositories;

public abstract class AbstractRepository
{
    protected ApplicationDbContext Context { get; init; }

    protected AbstractRepository(ApplicationDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }
}