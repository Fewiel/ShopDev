using Dapper;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class RoleRepository : RepositoryBase<Role>
{
    public RoleRepository(Database db) : base(db)
    {
    }

    public async Task ClearPermissionsAsync(long id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `RolePermissions` where `RoleId` = @id", new
        {
            id
        });
    }

    public async Task ClearUserPermissionsAsync(long id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `UserPermissions` where `UserId` = @id", new
        {
            id
        });
    }

    public async Task ClearLimitsAsync(long id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `RoleLimits` where `RoleId` = @id", new
        {
            id
        });
    }

    public async Task ClearUserLimitsAsync(long id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `UserLimits` where `UserId` = @id", new
        {
            id
        });
    }
}