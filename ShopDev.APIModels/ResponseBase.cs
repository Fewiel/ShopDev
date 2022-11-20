using ShopDev.APIModels.Models;

namespace ShopDev.APIModels;

public abstract class ResponseBase<TSelf> where TSelf : ResponseBase<TSelf>, new()
{
    public bool Success { get; set; }
    public List<Message>? Messages { get; set; }
    public Dictionary<string, string>? FormValidations { get; set; }

    public TSelf WithMessage(string msg, string type)
    {
        if (Messages == null)
            Messages = new List<Message>();

        Messages.Add(new(msg, type));
        return (TSelf)this;
    }

    public TSelf WithValidation(string field, string msg)
    {
        if (FormValidations == null)
            FormValidations = new Dictionary<string, string>();

        FormValidations.Add(field, msg);
        return (TSelf)this;
    }

    public TSelf WithValidation(Dictionary<string, string>? validations)
    {
        FormValidations = validations;
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

    public static TSelf Ok()
    {
        var t = new TSelf();
        t.Success = true;
        return t;
    }
}
