using System.Diagnostics;

namespace ShopDev.Server.Services;

public class DebugEmailProvider : IEmailProvider
{
    public Task<bool> SendMailAsync(string subject, string message, string recipient)
    {
        Debug.WriteLine($"[{DateTime.Now}][{nameof(DebugEmailProvider)}]: Mail To: {recipient}, Subject: {subject}\n{message}");
        return Task.FromResult(true);
    }
}