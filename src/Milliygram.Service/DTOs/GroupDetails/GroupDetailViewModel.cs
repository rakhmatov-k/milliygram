using Milliygram.Domain.Enums;
using Milliygram.Service.DTOs.ChatGroups;

namespace Milliygram.Service.DTOs.GroupDetails;

public class GroupDetailViewModel
{
    public long Id { get; set; }
    public ChatGroupViewModel ChatGroup { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public Privacy Privacy { get; set; }
}