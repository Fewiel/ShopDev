namespace ShopDev.APIModels.Models;

public class User : ResponseBase<User>
{
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? SlackID { get; set; }
    public bool Active { get; set; }
    public DateTime LastUsed { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Guid RoleID { get; set; }
    public string? RoleName { get; set; }
    public DateTime? AbsenceDate { get; set; }
    public string? AbsenceReason { get; set; }
    public string? AdminNote { get; set; }
}