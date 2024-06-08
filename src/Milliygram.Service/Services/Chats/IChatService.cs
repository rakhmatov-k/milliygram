using Milliygram.Service.DTOs.Chats;
using X.PagedList;

namespace Milliygram.Service.Services.Chats;

public interface IChatService
{
    Task<ChatViewModel> CreateAsync(ChatCreateModel createModel);
    Task<ChatViewModel> UpdateAsync(long id, ChatUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<ChatViewModel> GetByIdAsync(long id);
    Task<IPagedList<ChatViewModel>> GetAllAsync(int? page, string search = null);
}