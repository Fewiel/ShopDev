using ShopDev.DAL.Interfaces;

namespace ShopDev.DAL.Models;

public class User : IDBIdentifier
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? SlackId { get; set; }
    public string? Password { get; set; }
    public string? SSHPublicKey { get; set; }
    public bool Active { get; set; }
    public DateTime LastUsed { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Guid RoleId { get; set; }
    public DateTime AbsenceDate { get; set; }
    public string? AbsenceReason { get; set; }
    public string? AdminNote { get; set; }
}