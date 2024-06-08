using Milliygram.Service.DTOs.ChatMembers;
using X.PagedList;

namespace Milliygram.Service.Services.ChatMembers;

public interface IChatMemberService
{
    Task<ChatMemberViewModel> CreateAsync(ChatMemberCreateModel createModel);
    Task<ChatMemberViewModel> UpdateAsync(long id, ChatMemberUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<ChatMemberViewModel> GetByIdAsync(long id);
    Task<IPagedList<ChatMemberViewModel>> GetAllAsync(int? page, string search = null);
}