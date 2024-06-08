using Microsoft.EntityFrameworkCore;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Domain.Entities.Commons;
using Milliygram.Domain.Entities.Users;

namespace Milliygram.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<DbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatGroup> ChatGroups { get; set; }
    public DbSet<ChatMember> ChatMembers { get; set; }
    public DbSet<GroupDetail> GroupDetails { get; set; }
    public DbSet<Message> Messages { get; set; }
}