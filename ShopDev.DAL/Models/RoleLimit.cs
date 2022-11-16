namespace ShopDev.DAL.Models;

public class RoleLimit
{
    public Guid RoleId { get; set; }
    public Guid LimitId { get; set; }
    public int Value { get; set; }
}