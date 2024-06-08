using Milliygram.Data.Repositories;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Domain.Entities.Commons;
using Milliygram.Domain.Entities.Users;

namespace Milliygram.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Chat> Chats { get; }
    IRepository<Asset> Assets { get; }
    IRepository<Message> Messages { get; }
    IRepository<ChatGroup> ChatGroups { get; }
    IRepository<UserDetail> UserDetails { get; }
    IRepository<ChatMember> ChatMembers { get; }
    IRepository<GroupDetail> GroupDetails { get; }
    Task<bool> SaveAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
}