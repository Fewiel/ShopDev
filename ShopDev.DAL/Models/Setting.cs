using ShopDev.DAL.Interfaces;
using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Models;

public class Setting : IDBIdentifier
{
    public Guid ID { get; set; }
    public string? Key { get; set; }
    public string? DisplayName { get; set; }
    public string? Value { get; set; }
    public string DisplayType { get; set; } = "text";
}