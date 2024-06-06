﻿using Milliygram.Domain.Commons;
using Milliygram.Domain.Enums;

namespace Milliygram.Domain.Entities.Chats;

public class Chat : Auditable
{
    public ChatType ChatType { get; set; }
}