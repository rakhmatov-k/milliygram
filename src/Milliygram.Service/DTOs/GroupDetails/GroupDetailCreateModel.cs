﻿using Milliygram.Domain.Enums;

namespace Milliygram.Service.DTOs.GroupDetails;

public class GroupDetailCreateModel
{
    public long GroupId { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public Privacy Privacy { get; set; }
}