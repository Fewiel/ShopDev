using Dapper;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class UserLimitRepository
{
    private Database DB;

    public UserLimitRepository(Database db)
    {
        DB = db;
    }

    public async Task<IEnumerable<UserLimit>> GetForUserAsync(Guid id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QueryAsync<UserLimit>("select * from `UserLimits` where `UserId` = @id", new
        {
            id
        });
    }

    public async Task<UserLimit> GetForUserAndLimitAsync(Guid id, Guid lid)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QuerySingleOrDefaultAsync<UserLimit>("select * from `UserLimits` where `UserId` = @id and `LimitId` = @lid", new
        {
            id,
            lid
        });
    }

    public void Add(UserLimit ul)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        c.Connection.Execute("insert into `UsersLimits` (`UserId`, `LimitId`, `Value`) values (@uid, @lid, @value)", new
        {
            uid = ul.UserId,
            lid = ul.LimitId,
            value = ul.Value
        });
    }

    public void Update(UserLimit ul)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        c.Connection.Execute("update `UserLimits` set `Value` = @value where `UserId` = @uid and `LimitId` = @lid", new
        {
            uid = ul.UserId,
            lid = ul.LimitId,
            value = ul.Value
        });
    }

    public void Remove(UserLimit ul)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        c.Connection.Execute("delete from `UserLimits` where `UserId` = @uid and `LimitId` = @lid", new
        {
            uid = ul.UserId,
            lid = ul.LimitId
        });
    }
}