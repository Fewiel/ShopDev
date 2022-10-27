using ShopDev.DAL.Models;
using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Repositories;

public class SettingRepository : RepositoryBase<Setting>
{
    public SettingRepository(Database db) : base(db) { }

    protected override TableSchema TableSchema { get; } = new TableSchema("Settings", new()
    {
        new ColumnSchema(DBType.Binary, "ID") { IsPrimary = true },
        new ColumnSchema(DBType.Varchar, 64, "SettingKey"),
        new ColumnSchema(DBType.Text, "DisplayName"),
        new ColumnSchema(DBType.Text, "Value"),
        new ColumnSchema(DBType.Varchar, 16, "DisplayType")
    });
}