using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Interfaces;

public interface IDBIdentifier
{
    Guid ID { get; }

    object? GetFor(ColumnSchema col);
}