namespace ShopDev.DAL.Schema;

public class ColumnSchema
{
    public DBType Type { get; }
    public long Length { get; }
    public string Name { get; }

    public bool IsPrimary { get; init; }
    public bool IsIndex { get; init; }
    public bool IsUnique { get; init; }

    public string? Default { get; init; }
    public bool CanBeNull { get; init; }

    public ColumnSchema(DBType type, string name)
    {
        Type = type;
        Length = -1;
        Name = name;
    }

    public ColumnSchema(DBType type, long length, string name)
    {
        Type = type;
        Length = length;
        Name = name;
    }
}