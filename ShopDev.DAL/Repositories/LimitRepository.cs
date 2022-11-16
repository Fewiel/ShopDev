using Dapper;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class LimitRepository : RepositoryBase<Limit>
{
    public LimitRepository(Database db) : base(db)
    {
    }

    public async Task<Limit> GetByNameAsync(string internalName)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var limit = await c.Connection.QueryFirstOrDefaultAsync<Limit>("select * from `Limits` where `InternalName` = @internalName;", new
        {
            internalName
        });

        return limit;
    }

    public int GetLimit(User usr, string internalName)
    {
        var limit = GetByNameAsync(internalName);
        var userLimit = DB.UserLimit.GetForUserAndLimit(usr.Id, limit.Id);

        if (userLimit != null)
            return userLimit.Value;

        var roleLimit = DB.RoleLimit.GetForRoleAndLimit(usr.RoleId, limit.Id);

        if (roleLimit != null)
            return roleLimit.Value;

        return 0;
    }
}