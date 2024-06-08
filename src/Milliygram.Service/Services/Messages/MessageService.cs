using AutoMapper;
using Milliygram.Data.UnitOfWorks;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Service.DTOs.Messages;
using Milliygram.Service.Exceptions;
using Milliygram.Service.Extensions;
using X.PagedList;

namespace Milliygram.Service.Services.Messages;

public class MessageService
    (IMapper mapper,
    IUnitOfWork unitOfWork) : IMessageService
{
    public async Task<MessageViewModel> CreateAsync(MessageCreateModel createModel)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == createModel.SenderId)
            ?? throw new NotFoundException($"User not found with ID {createModel.SenderId}");

        var existChat = await unitOfWork.Chats.SelectAsync(c => c.Id == createModel.ChatId)
            ?? throw new NotFoundException($"Chat not found with ID {createModel.ChatId}");

        var mappedMessage = mapper.Map<Message>(createModel);
        mappedMessage.Create();

        var createdMessage = await unitOfWork.Messages.InsertAsync(mappedMessage);
        await unitOfWork.SaveAsync();

        return mapper.Map<MessageViewModel>(createdMessage);
    }

    public async Task<MessageViewModel> UpdateAsync(long id, MessageUpdateModel updateModel)
    {
        var existMessage = await unitOfWork.Messages.SelectAsync(m => m.Id == id)
            ?? throw new NotFoundException($"Message not found with ID {id}");

        mapper.Map(updateModel, existMessage);
        existMessage.Update();

        var updatedMessage = await unitOfWork.Messages.UpdateAsync(existMessage);
        await unitOfWork.SaveAsync();

        return mapper.Map<MessageViewModel>(updatedMessage);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existMessage = await unitOfWork.Messages.SelectAsync(m => m.Id == id)
            ?? throw new NotFoundException($"Message not found with ID {id}");

        existMessage.Delete();
        await unitOfWork.Messages.DeleteAsync(existMessage);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<MessageViewModel> GetByIdAsync(long id)
    {
        var existMessage = await unitOfWork.Messages.SelectAsync(m => m.Id == id, includes: ["User", "Chat", "Asset"])
            ?? throw new NotFoundException($"Message not found with ID {id}");

        return mapper.Map<MessageViewModel>(existMessage);
    }

    public async Task<IPagedList<MessageViewModel>> GetAllAsync(int? page, string search = null)
    {
        var messages = unitOfWork.Messages.SelectAsQueryable(includes: ["User", "Chat", "Asset"]);

        if (!string.IsNullOrWhiteSpace(search))
            messages = messages.Where(m => m.Content.ToLower().Contains(search.ToLower()));

        var pagedMessages = await messages.ToPagedListAsync(page ?? 1, 10);

        return mapper.Map<IPagedList<MessageViewModel>>(pagedMessages);
    }
}