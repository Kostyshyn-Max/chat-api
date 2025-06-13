using ChatApp.DataAccess.Entities;

namespace ChatApp.DataAccess.Interfaces;

public interface IUserRepository : ICrudRepository<User>
{
}