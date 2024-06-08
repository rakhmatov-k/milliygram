using Milliygram.Domain.Enums;

namespace Milliygram.Service.DTOs.GroupDetails;

public class GroupDetailViewModel
{
    public long Id { get; set; }
    public long GroupId { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public Privacy Privacy { get; set; }
}