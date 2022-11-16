using Dapper;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class RolePermissionRepository
{
    private Database DB;

    public RolePermissionRepository(Database db)
    {
        DB = db;
    }

    public async Task<IEnumerable<RolePermission>> GetForRoleAsync(long rid)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QueryAsync<RolePermission>("select * from `Role_Permissions` where `RoleId` = @rid", new
        {
            rid
        });
    }

    public async Task<RolePermission> GetForRoleAndPermissionAsync(long rid, long pid)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QuerySingleOrDefaultAsync<RolePermission>("select * from `Role_Permissions` where `RoleId` = @rid and `PermissionId` = @pid", new
        {
            rid,
            pid
        });
    }

    public async Task AddAsync(RolePermission rp)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("insert into `Role_Permissions` (`RoleId`, `PermissionId`) values (@rid, @pid)", new
        {
            rid = rp.RoleId,
            pid = rp.PermissionId
        });
    }

    public async Task RemoveAsync(RolePermission rp)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `Role_Permissions` where `RoleId` = @rid and `PermissionId` = @pid", new
        {
            rid = rp.RoleId,
            pid = rp.PermissionId
        });
    }
}