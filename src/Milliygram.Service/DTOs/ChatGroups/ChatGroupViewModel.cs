using Milliygram.Service.DTOs.Chats;

namespace Milliygram.Service.DTOs.ChatGroups;

public class ChatGroupViewModel
{
    public long Id { get; set; }
    public ChatViewModel Chat { get; set; }
    public string Name { get; set; }
}