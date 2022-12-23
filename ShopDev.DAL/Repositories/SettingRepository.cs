using DapperExtensions;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class SettingRepository : RepositoryBase<Setting>
{
    public SettingRepository(Models.Database db) : base(db) { }

    public async Task<Setting?> GetByKeyAsync(string key)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var predicate = Predicates.Field<Setting>(f => f.Key, Operator.Eq, key);
        return (await c.Connection.GetListAsync<Setting>(predicate)).FirstOrDefault();
    }
}