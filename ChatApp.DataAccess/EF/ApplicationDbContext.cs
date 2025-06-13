using ChatApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.DataAccess.EF;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }
}