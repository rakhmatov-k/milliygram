using Microsoft.EntityFrameworkCore.Storage;
using Milliygram.Data.DbContexts;
using Milliygram.Data.Repositories;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Domain.Entities.Commons;
using Milliygram.Domain.Entities.Users;

namespace Milliygram.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    private IDbContextTransaction transaction;
    public IRepository<User> Users { get; }

    public IRepository<Chat> Chats { get; }

    public IRepository<Asset> Assets { get; }

    public IRepository<Message> Messages { get; }

    public IRepository<ChatGroup> ChatGroups { get; }

    public IRepository<UserDetail> UserDetails { get; }

    public IRepository<ChatMember> ChatMembers { get; }

    public IRepository<GroupDetail> GroupDetails { get; }

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Users = new Repository<User>(context);
        Chats = new Repository<Chat>(context);
        Assets = new Repository<Asset>(context);
        Messages = new Repository<Message>(context);
        ChatGroups = new Repository<ChatGroup>(context);
        UserDetails = new Repository<UserDetail>(context);
        ChatMembers = new Repository<ChatMember>(context);
        GroupDetails = new Repository<GroupDetail>(context);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task BeginTransactionAsync()
    {
        transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }
}