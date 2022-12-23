namespace ShopDev.APIModels.User;

public class AddUserResponse : ResponseBase<AddUserResponse>
{
    public AddUserResponseReason Reason { get; set; }

    public AddUserResponse WithReason(AddUserResponseReason reason)
    {
        Reason = reason;
        return this;
    }
}

public enum AddUserResponseReason
{
    MissingUsername,
    UserExists,
    MissingRole,
    MissingMail
}