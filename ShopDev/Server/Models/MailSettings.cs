namespace ShopDev.Server.Models;

public class MailSettings
{
    public int Port { get; set; }
    public string Server { get; set; }
    public string Sender { get; set; }
    public string SMTPUser { get; set; }
    public string SMTPPassword { get; set; }
    public bool Ssl { get; set; }
}