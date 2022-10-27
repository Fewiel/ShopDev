using Google.Protobuf.WellKnownTypes;
using ShopDev.DAL.Interfaces;
using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Models;

public class Log : IDBIdentifier
{
    public Guid ID { get; set; }
    public string? Source { get; set; }
    public string? Message { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public object? GetFor(ColumnSchema col)
    {
        switch (col.Name)
        {
            case "ID": return ID;
            case "Source": return Source;
            case "Message": return Message;
            case "Timestamp": return Timestamp.UtcDateTime;
            default: return null;
        }
    }
}