using DapperExtensions;
using ShopDev.DAL.Interfaces;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public abstract class RepositoryBase<T> where T : class, IDBIdentifier
{
    protected Models.Database DB { get; }

    protected RepositoryBase(Models.Database db)
    {
        DB = db;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.GetListAsync<T>();
    }

    public async Task<T> GetByIDAsync(Guid id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.GetAsync<T>(id);
    }

    public async Task InsertAsync(T t) 
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.InsertAsync(t);
    }

    public async Task UpdateAsync(T t)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.UpdateAsync(t);
    }

    public async Task DeleteAsync(T t)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.DeleteAsync(t);
    }
}