using Milliygram.Service.DTOs.ChatGroups;
using X.PagedList;

namespace Milliygram.Service.Services.ChatGroups;

public interface IChatGroupService
{
    Task<ChatGroupViewModel> CreateAsync(ChatGroupCreateModel createModel);
    Task<ChatGroupViewModel> UpdateAsync(long id, ChatGroupUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<ChatGroupViewModel> GetByIdAsync(long id);
    Task<IPagedList<ChatGroupViewModel>> GetAllAsync(int? page, string search = null);
}