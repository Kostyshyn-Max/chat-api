using ChatApp.DataAccess.Entities;

namespace ChatApp.DataAccess.Interfaces;

public interface IChatRepository : ICrudRepository<Chat>
{
}