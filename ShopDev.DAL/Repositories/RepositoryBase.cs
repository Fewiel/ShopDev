using Dapper;
using ShopDev.DAL.Interfaces;
using ShopDev.DAL.Models;
using ShopDev.DAL.Schema;
using ShopDev.DAL.Utility;
using System.Text;

namespace ShopDev.DAL.Repositories;

public abstract class RepositoryBase<T> where T : IDBIdentifier
{
    protected abstract TableSchema TableSchema { get; }
    protected Database DB { get; }

    protected RepositoryBase(Database db)
    {
        DB = db;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QueryAsync<T>($"select * from `{TableSchema.Name}`;");
    }

    public async Task<T> GetByIDAsync(Guid id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QuerySingleOrDefaultAsync<T>($"select * from `{TableSchema.Name}` where ID = @id;", new
        {
            id
        });
    }

    public async Task InsertAsync(T t)
    {
        var sb = new StringBuilder();
        sb.Append("INSERT INTO `").Append(TableSchema.Name).Append("` (");

        var i = 0;
        foreach (var col in TableSchema.Columns)
        {
            i++;

            sb.Append('`').Append(col.Name).Append('`');

            if (i < TableSchema.Columns.Count)
                sb.Append(", ");
        }
        sb.Append(") VALUES (");

        var parameters = new Dictionary<string, object>();

        i = 0;
        foreach (var col in TableSchema.Columns)
        {
            i++;

            sb.Append('@').Append(col.Name);
            parameters.Add("@" + col.Name, t.GetFor(col)!);

            if (i < TableSchema.Columns.Count)
                sb.Append(", ");
        }
        sb.Append(");");

        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync(sb.ToString(), new DynamicParameters(parameters));
    }

    public async Task UpdateAsync(T t)
    {
        var sb = new StringBuilder();
        sb.Append("UPDATE `").Append(TableSchema.Name).Append("` SET ");

        var parameters = new Dictionary<string, object>();

        var i = 0;
        foreach (var col in TableSchema.Columns)
        {
            i++;

            sb.Append('`').Append(col.Name).Append("` = @").Append(col.Name);
            parameters.Add("@" + col.Name, t.GetFor(col)!);

            if (i < TableSchema.Columns.Count)
                sb.Append(", ");
        }

        sb.Append(" WHERE ID = @ID;");

        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync(sb.ToString(), new DynamicParameters(parameters));
    }

    public void Delete(T t)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        c.Connection.Execute($"DELETE FROM {TableSchema.Name} WHERE ID = @id;", new
        {
            id = t.ID
        });
    }
}