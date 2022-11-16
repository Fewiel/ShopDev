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
        await c.Connection.ExecuteAsync("delete from `Role_Permissions` where `RoleId` = @id", new
        {
            id
        });
    }

    public async Task ClearUserPermissionsAsync(long id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `User_Permissions` where `UserId` = @id", new
        {
            id
        });
    }

    public async Task ClearLimitsAsync(long id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `Role_Limits` where `RoleId` = @id", new
        {
            id
        });
    }

    public async Task ClearUserLimitsAsync(long id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `User_Limits` where `UserId` = @id", new
        {
            id
        });
    }
}