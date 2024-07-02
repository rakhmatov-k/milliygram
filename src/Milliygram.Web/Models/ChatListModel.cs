using Milliygram.Service.DTOs.Chats;

namespace Milliygram.Web.Models;

public class ChatListModel
{
    public string Search {  get; set; }
    public IEnumerable<ChatViewModel> Chats { get; set; }
}