using System.Net;

namespace ShopDev.DAL.Models;

public class NodeDefinition
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public IPAddress IP { get; set; } = null!;
    public bool Active { get; set; }
    public DateTime? LastConnected { get; set; }
}