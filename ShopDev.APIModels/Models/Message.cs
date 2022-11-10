namespace ShopDev.APIModels.Models;

public class Message
{
    public string Content { get; set; }
    public string Type { get; set; }

    public Message(string content, string type)
    {
        Content = content;
        Type = type;
    }
}