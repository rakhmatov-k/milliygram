using Microsoft.EntityFrameworkCore;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Domain.Entities.Commons;
using Milliygram.Domain.Entities.Users;
using System.Reflection;

namespace Milliygram.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatGroup> ChatGroups { get; set; }
    public DbSet<ChatMember> ChatMembers { get; set; }
    public DbSet<GroupDetail> GroupDetails { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>().HasData(new Asset
        {
            Id = 1,
            Name = "Default_Images",
            Path = "~/assets/Images/" + "Default_Images",
            FileType = Domain.Enums.FileType.Images
        });

        modelBuilder.Entity<User>().HasQueryFilter(user => !user.IsDeleted);
    }
}