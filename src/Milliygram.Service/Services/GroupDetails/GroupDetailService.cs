using AutoMapper;
using Milliygram.Data.UnitOfWorks;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Service.DTOs.GroupDetails;
using Milliygram.Service.Exceptions;
using Milliygram.Service.Extensions;
using X.PagedList;

namespace Milliygram.Service.Services.GroupDetails;

public class GroupDetailService
    (IMapper mapper,
    IUnitOfWork unitOfWork) : IGroupDetailService
{
    public async Task<GroupDetailViewModel> CreateAsync(GroupDetailCreateModel createModel)
    {
        var existChatGroup = await unitOfWork.ChatGroups.SelectAsync(g => g.Id == createModel.GroupId)
            ?? throw new NotFoundException($"ChatGroup not found with ID {createModel.GroupId}");

        var mappedGroupDetail = mapper.Map<GroupDetail>(createModel);
        mappedGroupDetail.Create();

        var createdGroupDetail = await unitOfWork.GroupDetails.InsertAsync(mappedGroupDetail);
        await unitOfWork.SaveAsync();

        return mapper.Map<GroupDetailViewModel>(createdGroupDetail);
    }

    public async Task<GroupDetailViewModel> UpdateAsync(long id, GroupDetailUpdateModel updateModel)
    {
        var existGroupDetail = await unitOfWork.GroupDetails.SelectAsync(gd => gd.Id == id)
            ?? throw new NotFoundException($"GroupDetail not found with ID {id}");

        var existChatGroup = await unitOfWork.ChatGroups.SelectAsync(g => g.Id == updateModel.GroupId)
            ?? throw new NotFoundException($"ChatGroup not found with ID {updateModel.GroupId}");

        mapper.Map(updateModel, existGroupDetail);
        existGroupDetail.Update();

        var updatedGroupDetail = await unitOfWork.GroupDetails.UpdateAsync(existGroupDetail);
        await unitOfWork.SaveAsync();

        return mapper.Map<GroupDetailViewModel>(updatedGroupDetail);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existGroupDetail = await unitOfWork.GroupDetails.SelectAsync(gd => gd.Id == id)
            ?? throw new NotFoundException($"GroupDetail not found with ID {id}");

        existGroupDetail.Delete();
        await unitOfWork.GroupDetails.DeleteAsync(existGroupDetail);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<GroupDetailViewModel> GetByIdAsync(long id)
    {
        var existGroupDetail = await unitOfWork.GroupDetails.SelectAsync(gd => gd.Id == id, includes: ["ChatGroup", "Picture"])
            ?? throw new NotFoundException($"GroupDetail not found with ID {id}");

        return mapper.Map<GroupDetailViewModel>(existGroupDetail);
    }

    public async Task<IPagedList<GroupDetailViewModel>> GetAllAsync(int? page)
    {
        var groupDetails = unitOfWork.GroupDetails.SelectAsQueryable(includes: ["ChatGroup", "Picture"]);

        var pagedGroupDetails = await groupDetails.ToPagedListAsync(page ?? 1, 10);

        return mapper.Map<IPagedList<GroupDetailViewModel>>(pagedGroupDetails);
    }
}