using Milliygram.Service.DTOs.GroupDetails;
using X.PagedList;

namespace Milliygram.Service.Services.GroupDetails;

public interface IGroupDetailService
{
    Task<GroupDetailViewModel> CreateAsync(GroupDetailCreateModel createModel);
    Task<GroupDetailViewModel> UpdateAsync(long id, GroupDetailUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<GroupDetailViewModel> GetByIdAsync(long id);
    Task<IPagedList<GroupDetailViewModel>> GetAllAsync(int? page, string search = null);
}