using ShopDev.DAL.Models;
using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Repositories;

public class LogRepository : RepositoryBase<Log>
{
    public LogRepository(Database db) : base(db)
    {
    }

    protected override TableSchema TableSchema { get; } = new TableSchema("Logs", new()
    {
        new ColumnSchema(DBType.Char, 36, "ID") { IsPrimary = true },
        new ColumnSchema(DBType.Text, "Source"),
        new ColumnSchema(DBType.Text, "Message"),
        new ColumnSchema(DBType.DateTime, "Timestamp")
    });
}