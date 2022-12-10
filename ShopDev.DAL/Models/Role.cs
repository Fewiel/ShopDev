using ShopDev.DAL.Interfaces;

namespace ShopDev.DAL.Models;

public class Role : IDBIdentifier
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}