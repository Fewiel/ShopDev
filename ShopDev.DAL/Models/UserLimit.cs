using ShopDev.DAL.Interfaces;

namespace ShopDev.DAL.Models;

public class UserLimit
{
    public Guid UserId { get; set; }
    public Guid LimitId { get; set; }
    public int Value { get; set; }
}