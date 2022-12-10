using Dapper;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class RoleLimitRepository
{
	private Database DB;

	public RoleLimitRepository(Database db)
	{
		DB = db;
	}

    public async Task<IEnumerable<RoleLimit>> GetForRoleAsync(Guid roleID)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QueryAsync<RoleLimit>("select * from `RoleLimits` where `RoleId` = @roleID", new
        {
            roleID
        });
    }

    public async Task<RoleLimit> GetForRoleAndLimitAsync(Guid roleID, Guid limitID)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QuerySingleOrDefaultAsync<RoleLimit>("select * from `RoleLimits` where `RoleId` = @roleID and `LimitId` = @limitID", new
        {
            roleID,
            limitID
        });
    }

    public async Task AddAsync(RoleLimit rl)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("insert into `RoleLimits` (`RoleId`, `LimitId`, `Value`) values (@rid, @lid, @value)", new
        {
            rid = rl.RoleId,
            lid = rl.LimitId,
            value = rl.Value
        });
    }

    public async Task UpdateAsync(RoleLimit rl)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("update `RoleLimits` set `Value` @value where `RoleId` = @rid and `LimitId` = @lid", new
        {
            rid = rl.RoleId,
            lid = rl.LimitId,
            value = rl.Value
        });
    }

    public async Task RemoveAsync(RoleLimit rl)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `RoleLimits` where `RoleId` = @rid and `LimitId` = @lid)", new
        {
            rid = rl.RoleId,
            lid = rl.LimitId
        });
    }

    public async Task RemoveForRoleAsync(long rid)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `RoleLimits` where `RoleId` = @rid)", new
        {
            rid
        });
    }
}