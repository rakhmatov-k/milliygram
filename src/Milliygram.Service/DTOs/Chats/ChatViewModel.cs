using Milliygram.Domain.Enums;
using Milliygram.Service.DTOs.ChatGroups;
using Milliygram.Service.DTOs.ChatMembers;
using Milliygram.Service.DTOs.Messages;

namespace Milliygram.Service.DTOs.Chats;

public class ChatViewModel
{
    public long Id { get; set; }
    public ChatType ChatType { get; set; }
    public IEnumerable<ChatGroupViewModel> ChatGroups { get; set; }
    public IEnumerable<ChatMemberViewModel> ChatMembers { get; set; }
    public IEnumerable<MessageViewModel> Messages { get; set; }  
}