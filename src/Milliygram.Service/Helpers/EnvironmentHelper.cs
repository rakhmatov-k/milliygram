namespace Milliygram.Service.Helpers;

public static class EnvironmentHelper
{
    public static string WebRootPath { get; set; }
    public static string EmailAddress { get; set; }
    public static string EmailPassword { get; set; }
    public static string SmtpHost { get; set; }
    public static string SmtpPort { get; set; }
}