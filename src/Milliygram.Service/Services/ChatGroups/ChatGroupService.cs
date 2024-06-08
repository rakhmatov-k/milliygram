using AutoMapper;
using Milliygram.Data.UnitOfWorks;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Service.DTOs.ChatGroups;
using Milliygram.Service.Exceptions;
using Milliygram.Service.Extensions;
using X.PagedList;

namespace Milliygram.Service.Services.ChatGroups;

public class ChatGroupService 
    (IMapper mapper,
    IUnitOfWork unitOfWork) : IChatGroupService
{
    public async Task<ChatGroupViewModel> CreateAsync(ChatGroupCreateModel createModel)
    {
        var existChat = await unitOfWork.Chats.SelectAsync(c => c.Id == createModel.ChatId)
            ?? throw new NotFoundException($"Chat not found with ID {createModel.ChatId}");

        var existChatGroup = await unitOfWork.ChatGroups
            .SelectAsync(g => g.ChatId == createModel.ChatId && g.Name.ToLower() == createModel.Name.ToLower());

        if (existChatGroup is not null)
            throw new AlreadyExistException($"Group is already exist with this name :{createModel.Name}");

        var mappedChatGroup = mapper.Map<ChatGroup>(createModel);
        mappedChatGroup.Create();

        var createdChatGroup = await unitOfWork.ChatGroups.InsertAsync(mappedChatGroup);
        await unitOfWork.SaveAsync();

        return mapper.Map<ChatGroupViewModel>(createdChatGroup);
    }

    public async Task<ChatGroupViewModel> UpdateAsync(long id, ChatGroupUpdateModel updateModel)
    {
        var existChatGroup = await unitOfWork.ChatGroups.SelectAsync(cg => cg.Id == id)
            ?? throw new NotFoundException($"ChatGroup not found with ID {id}");

        var existChat = await unitOfWork.Chats.SelectAsync(c => c.Id == updateModel.ChatId)
           ?? throw new NotFoundException($"Chat not found with ID {updateModel.ChatId}");

        var alreadyExistChatGroup = await unitOfWork.ChatGroups
           .SelectAsync(g => g.ChatId == updateModel.ChatId && g.Name.ToLower() == updateModel.Name.ToLower() && g.Id != id);

        if (alreadyExistChatGroup is not null)
            throw new AlreadyExistException($"Group is already exist with this name :{updateModel.Name}");

        mapper.Map(updateModel, existChatGroup);
        existChatGroup.Update();

        var updatedChatGroup = await unitOfWork.ChatGroups.UpdateAsync(existChatGroup);
        await unitOfWork.SaveAsync();

        return mapper.Map<ChatGroupViewModel>(updatedChatGroup);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existChatGroup = await unitOfWork.ChatGroups.SelectAsync(cg => cg.Id == id)
            ?? throw new NotFoundException($"ChatGroup not found with ID {id}");

        existChatGroup.Delete();
        await unitOfWork.ChatGroups.DeleteAsync(existChatGroup);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<ChatGroupViewModel> GetByIdAsync(long id)
    {
        var existChatGroup = await unitOfWork.ChatGroups.SelectAsync(cg => cg.Id == id, includes: ["Chat"])
            ?? throw new NotFoundException($"ChatGroup not found with ID {id}");

        return mapper.Map<ChatGroupViewModel>(existChatGroup);
    }

    public async Task<IPagedList<ChatGroupViewModel>> GetAllAsync(int? page, string search = null)
    {
        var chatGroups = unitOfWork.ChatGroups.SelectAsQueryable(includes: ["Chat"]);

        if (!string.IsNullOrWhiteSpace(search))
            chatGroups = chatGroups.Where(cg => cg.Name.ToLower().Contains(search.ToLower()));

        var pagedChatGroups = await chatGroups.ToPagedListAsync(page ?? 1, 10);

        return mapper.Map<IPagedList<ChatGroupViewModel>>(pagedChatGroups);
    }
}