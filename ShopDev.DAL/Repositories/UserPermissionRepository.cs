using Dapper;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class UserPermissionRepository
{
	private Database DB;

	public UserPermissionRepository(Database db)
	{
		DB = db;
	}

    public async Task<IEnumerable<UserPermission>> GetForUserAsync(long id)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QueryAsync<UserPermission>("select * from `User_Permissions` where `UserId` = @id", new
        {
            id
        });
    }

    public async Task AddAsync(UserPermission up)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("insert into `User_Permissions` (`UserId`, `PermissionId`) values (@uid, @pid)", new
        {
            uid = up.UserId,
            pid = up.PermissionId
        });
    }

    public async Task RemoveAsync(UserPermission up)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        await c.Connection.ExecuteAsync("delete from `Users_Permissions` where `UserId` = @uid and `PermissionId` = @pid", new
        {
            uid = up.UserId,
            pid = up.PermissionId
        });
    }
}