using Milliygram.Data.UnitOfWorks;
using Milliygram.Service.Helpers;
using Milliygram.Service.Services.Assets;
using Milliygram.Service.Services.Chats;
using Milliygram.Service.Services.UserDetails;
using Milliygram.Service.Services.Users;

namespace Milliygram.Web.Extensions;

public static class CollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserDetailService, UserDetailService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IAssetService, AssetService>();
    }
    public static void InjectEnvironmentItems(this WebApplication app)
    {
        EnvironmentHelper.WebRootPath = Path.GetFullPath("wwwroot");
    }
}