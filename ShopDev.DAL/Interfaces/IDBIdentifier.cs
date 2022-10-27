using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Interfaces;

public interface IDBIdentifier
{
    Guid Id { get; }
}