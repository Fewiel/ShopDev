namespace ShopDev.Server.Services;

public interface IEmailProvider
{
    Task<bool> SendMailAsync(string subject, string message, string recipient);
}