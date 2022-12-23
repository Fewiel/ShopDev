using System.Collections.Concurrent;
using System.Net.Mail;
using System.Net;
using ShopDev.Server.Models;

namespace ShopDev.Server.Services;

public class EmailProvider : IEmailProvider
{
    private readonly MailSettings Settings;

    public EmailProvider(MailSettings settings)
    {
        Settings = settings;
    }

    public Task<bool> SendMailAsync(string subject, string body, string recipient)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(Settings.Sender, "ShopDev"),
            Subject = "[System Message] " + subject,
            Body = body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(new MailAddress(recipient));

        Task.Factory.StartNew(() =>
        {
            var smtpClient = new SmtpClient(Settings.Server)
            {
                Port = Settings.Port,
                Credentials = new NetworkCredential(Settings.SMTPUser, Settings.SMTPPassword),
                EnableSsl = Settings.Ssl
            };
            smtpClient.Send(mailMessage);
        });

        return Task.FromResult(true);
    }
}