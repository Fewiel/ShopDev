using Google.Protobuf.WellKnownTypes;
using ShopDev.DAL.Interfaces;
using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Models;

public class Log : IDBIdentifier
{
    public Guid Id { get; set; }
    public string? Source { get; set; }
    public string? Message { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}