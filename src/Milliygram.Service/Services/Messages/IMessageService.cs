using Milliygram.Service.DTOs.Messages;
using X.PagedList;

namespace Milliygram.Service.Services.Messages;

public interface IMessageService
{
    Task<MessageViewModel> CreateAsync(MessageCreateModel createModel);
    Task<MessageViewModel> UpdateAsync(long id, MessageUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<MessageViewModel> GetByIdAsync(long id);
    Task<IPagedList<MessageViewModel>> GetAllAsync(int? page, string search = null);
}