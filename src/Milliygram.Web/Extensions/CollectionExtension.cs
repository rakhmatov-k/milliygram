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
        EnvironmentHelper.SmtpHost = app.Configuration.GetSection("Email:Host").Value;
        EnvironmentHelper.SmtpPort = app.Configuration.GetSection("Email:Port").Value;
        EnvironmentHelper.EmailAddress = app.Configuration.GetSection("Email:EmailAddress").Value;
        EnvironmentHelper.EmailPassword = app.Configuration.GetSection("Email:Password").Value;
    }
}