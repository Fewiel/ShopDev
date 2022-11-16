using ShopDev.DAL.Interfaces;

namespace ShopDev.DAL.Models;

public class Limit : IDBIdentifier
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? InternalName { get; set; }
}