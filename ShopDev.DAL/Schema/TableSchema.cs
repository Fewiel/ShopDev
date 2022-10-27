namespace ShopDev.DAL.Schema;

public class TableSchema
{
    public string Name { get; }
    public List<ColumnSchema> Columns { get; }

    public TableSchema(string name, List<ColumnSchema> columns)
    {
        Name = name;
        Columns = columns;
    }
}