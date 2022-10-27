using Google.Protobuf.WellKnownTypes;
using Google.Protobuf;
using ShopDev.DAL.Interfaces;
using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Models;

public class User : IDBIdentifier
{
    public Guid ID { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? SlackID { get; set; }
    public string? Password { get; set; }
    public string? SSHPublicKey { get; set; }
    public bool Active { get; set; }
    public DateTimeOffset LastUsed { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public Guid RoleID { get; set; }
    public DateTimeOffset AbsenceDate { get; set; }
    public string? AbsenceReason { get; set; }
    public string? AdminNote { get; set; }

    public object? GetFor(ColumnSchema col)
    {
        switch (col.Name)
        {
            case "ID": return ID;
            case "Email": return Email;
            case "Username": return Username;
            case "Name": return Name;
            case "SlackID": return SlackID;
            case "Password": return Password;
            case "SSHPublicKey": return SSHPublicKey;
            case "Active": return Active;
            case "LastUsed": return LastUsed.UtcDateTime;
            case "ExpirationDate": return ExpirationDate.UtcDateTime;
            case "RoleID": return RoleID;
            case "AbsenceDate": return AbsenceDate.UtcDateTime;
            case "AbsenceReason": return AbsenceReason;
            case "AdminNote": return AdminNote;
            default: return null;
        }
    }
}