using Milliygram.Data.UnitOfWorks;
using Milliygram.Service.Services.Users;

namespace Milliygram.Web.Extensions;

public static class CollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
    }
}