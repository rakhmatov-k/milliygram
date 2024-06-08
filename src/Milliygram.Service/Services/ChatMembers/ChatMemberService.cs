using AutoMapper;
using Milliygram.Data.UnitOfWorks;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Service.DTOs.ChatMembers;
using Milliygram.Service.Exceptions;
using Milliygram.Service.Extensions;
using X.PagedList;

namespace Milliygram.Service.Services.ChatMembers;

public class ChatMemberService 
    (IMapper mapper,
    IUnitOfWork unitOfWork): IChatMemberService
{
    public async Task<ChatMemberViewModel> CreateAsync(ChatMemberCreateModel createModel)
    {
        var existChat = await unitOfWork.Chats.SelectAsync(c => c.Id == createModel.ChatId)
            ?? throw new NotFoundException($"Chat not found with ID {createModel.ChatId}");

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == createModel.UserId)
            ?? throw new NotFoundException($"User not found with ID {createModel.UserId}");

        var existChatMember = await unitOfWork.ChatMembers
            .SelectAsync(cm => cm.ChatId == createModel.ChatId && cm.UserId == createModel.UserId);

        if (existChatMember is not null)
            throw new AlreadyExistException($"User is already a member of the chat.");

        var mappedChatMember = mapper.Map<ChatMember>(createModel);
        mappedChatMember.Create();

        var createdChatMember = await unitOfWork.ChatMembers.InsertAsync(mappedChatMember);
        await unitOfWork.SaveAsync();

        return mapper.Map<ChatMemberViewModel>(createdChatMember);
    }

    public async Task<ChatMemberViewModel> UpdateAsync(long id, ChatMemberUpdateModel updateModel)
    {
        var existChatMember = await unitOfWork.ChatMembers.SelectAsync(cm => cm.Id == id)
            ?? throw new NotFoundException($"ChatMember not found with ID {id}");

        var existChat = await unitOfWork.Chats.SelectAsync(c => c.Id == updateModel.ChatId)
            ?? throw new NotFoundException($"Chat not found with ID {updateModel.ChatId}");

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == updateModel.UserId)
            ?? throw new NotFoundException($"User not found with ID {updateModel.UserId}");

        var alreadyExistChatMember = await unitOfWork.ChatMembers
            .SelectAsync(cm => cm.ChatId == updateModel.ChatId && cm.UserId == updateModel.UserId && cm.Id != id);

        if (alreadyExistChatMember is not null)
            throw new AlreadyExistException($"User is already a member of the chat.");

        mapper.Map(updateModel, existChatMember);
        existChatMember.Update();

        var updatedChatMember = await unitOfWork.ChatMembers.UpdateAsync(existChatMember);
        await unitOfWork.SaveAsync();

        return mapper.Map<ChatMemberViewModel>(updatedChatMember);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existChatMember = await unitOfWork.ChatMembers.SelectAsync(cm => cm.Id == id)
            ?? throw new NotFoundException($"ChatMember not found with ID {id}");

        existChatMember.Delete();
        await unitOfWork.ChatMembers.DeleteAsync(existChatMember);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<ChatMemberViewModel> GetByIdAsync(long id)
    {
        var existChatMember = await unitOfWork.ChatMembers.SelectAsync(cm => cm.Id == id, includes: ["Chat", "User"])
            ?? throw new NotFoundException($"ChatMember not found with ID {id}");

        return mapper.Map<ChatMemberViewModel>(existChatMember);
    }

    public async Task<IPagedList<ChatMemberViewModel>> GetAllAsync(int? page, string search = null)
    {
        var chatMembers = unitOfWork.ChatMembers.SelectAsQueryable(includes: ["Chat", "User"]);

        var pagedChatMembers = await chatMembers.ToPagedListAsync(page ?? 1, 10);

        return mapper.Map<IPagedList<ChatMemberViewModel>>(pagedChatMembers);
    }
}