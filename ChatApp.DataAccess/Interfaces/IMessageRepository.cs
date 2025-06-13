using ChatApp.DataAccess.Entities;

namespace ChatApp.DataAccess.Interfaces;

public interface IMessageRepository : ICrudRepository<Message>
{
}