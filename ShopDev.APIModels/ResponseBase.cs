namespace ShopDev.APIModels;

public abstract class ResponseBase<TSelf> where TSelf : ResponseBase<TSelf>, new()
{
    public bool Success { get; set; }
    public Dictionary<string, string>? Messages { get; set; }

    public TSelf WithMessage(string msg, string type)
    {
        if (Messages == null)
            Messages = new Dictionary<string, string>();
        
        Messages.Add(msg, type);
        return (TSelf)this;
    }

    public static TSelf Fail() => new TSelf();

    public static TSelf Ok(Action<TSelf> configure)
    {
        var t = new TSelf();
        t.Success = true;
        configure(t);
        return t;
    }
}
