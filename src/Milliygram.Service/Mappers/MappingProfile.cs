using AutoMapper;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Domain.Entities.Commons;
using Milliygram.Domain.Entities.Users;
using Milliygram.Service.DTOs.Assets;
using Milliygram.Service.DTOs.ChatGroups;
using Milliygram.Service.DTOs.ChatMembers;
using Milliygram.Service.DTOs.Chats;
using Milliygram.Service.DTOs.GroupDetails;
using Milliygram.Service.DTOs.Messages;
using Milliygram.Service.DTOs.UserDetails;
using Milliygram.Service.DTOs.Users;

namespace Milliygram.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Asset, AssetViewModel>().ReverseMap();

        CreateMap<UserCreateModel, User>().ReverseMap();
        CreateMap<UserUpdateModel, User>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<UserUpdateModel, UserViewModel>().ReverseMap();

        CreateMap<UserDetailCreateModel, UserDetail>().ReverseMap();
        CreateMap<UserDetailUpdateModel, UserDetail>().ReverseMap();
        CreateMap<UserDetail, UserDetailViewModel>().ReverseMap();
        CreateMap<UserDetailUpdateModel, UserDetailViewModel>().ReverseMap();

        CreateMap<ChatCreateModel, Chat>().ReverseMap();
        CreateMap<ChatUpdateModel, Chat>().ReverseMap();
        CreateMap<Chat, ChatViewModel>().ReverseMap();

        CreateMap<ChatGroupCreateModel, ChatGroup>().ReverseMap();
        CreateMap<ChatGroupUpdateModel, ChatGroup>().ReverseMap();
        CreateMap<ChatGroup, ChatGroupViewModel>().ReverseMap();

        CreateMap<GroupDetailCreateModel, GroupDetail>().ReverseMap();
        CreateMap<GroupDetailUpdateModel, GroupDetail>().ReverseMap();
        CreateMap<GroupDetail, GroupDetailViewModel>().ReverseMap();

        CreateMap<ChatMemberCreateModel, ChatMember>().ReverseMap();
        CreateMap<ChatMemberUpdateModel, ChatMember>().ReverseMap();
        CreateMap<ChatMember, ChatMemberViewModel>().ReverseMap();

        CreateMap<MessageCreateModel, Message>().ReverseMap();
        CreateMap<MessageUpdateModel, Message>().ReverseMap();
        CreateMap<Message, MessageViewModel>().ReverseMap();
    }
}