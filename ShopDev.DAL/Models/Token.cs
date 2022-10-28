using ShopDev.DAL.Interfaces;

namespace ShopDev.DAL.Models;

public class Token : IDBIdentifier
{
    public Guid Id { get; set; }
    public Guid UserID { get; set; }
    public DateTime ExpireTime { get; set; }
    public TokenType Type { get; set; }
}

public enum TokenType
{
    Login,
    PasswordRecovery,
    SetSSHKey
}