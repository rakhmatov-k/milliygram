using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Milliygram.Data.UnitOfWorks;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Service.DTOs.Chats;
using Milliygram.Service.Exceptions;
using Milliygram.Service.Extensions;

namespace Milliygram.Service.Services.Chats;
public class ChatService
    (IMapper mapper,
    IUnitOfWork unitOfWork) : IChatService
{
    public async Task<ChatViewModel> CreateAsync(ChatCreateModel createModel)
    {
        var mappedChat = mapper.Map<Chat>(createModel);
        mappedChat.Create();

        var createdChat = await unitOfWork.Chats.InsertAsync(mappedChat);
        await unitOfWork.SaveAsync();

        return mapper.Map<ChatViewModel>(createdChat);
    }

    public async Task<ChatViewModel> UpdateAsync(long id, ChatUpdateModel updateModel)
    {
        var existChat = await unitOfWork.Chats.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Chat is not found with this ID {id}");

        mapper.Map(updateModel, existChat);
        existChat.Update();

        var updatedChat = await unitOfWork.Chats.UpdateAsync(existChat);
        await unitOfWork.SaveAsync();

        return mapper.Map<ChatViewModel>(updatedChat);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existChat = await unitOfWork.Chats.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException($"Chat is not found with this ID {id}");

        existChat.Delete();
        await unitOfWork.Chats.DeleteAsync(existChat);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<ChatViewModel> GetByIdAsync(long id)
    {
        var existChat = await unitOfWork.Chats.SelectAsync(c => c.Id == id, includes: ["ChatGroups", "ChatMembers", "Messages"])
            ?? throw new NotFoundException($"Chat is not found with this ID {id}");

        return mapper.Map<ChatViewModel>(existChat);
    }

    public async Task<IEnumerable<ChatViewModel>> GetAllAsync(long UserId, string search)
    {
        var chats = unitOfWork.Chats
            .SelectAsQueryable(expression:ch => ch.UserId == UserId, includes: ["ChatGroups", "ChatMembers", "Messages"]);

        if (!string.IsNullOrWhiteSpace(search))
            chats = chats.Where(c =>
                c.User.UserName.ToLower().Contains(search.ToLower()) ||
                c.ChatGroups.Any(g => g.Name.ToLower().Contains(search.ToLower())) ||
                c.ChatMembers.Any(m => m.User.UserName.ToLower().Contains(search.ToLower())));

        return mapper.Map<IEnumerable<ChatViewModel>>(await chats.ToListAsync());
    }
}