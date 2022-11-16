using Dapper;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class PermissionRepository : RepositoryBase<Permission>
{
    private readonly UserRepository _userRepository;

    public PermissionRepository(Database db, UserRepository userRepository) : base(db) 
    { 
        _userRepository = userRepository;
    }


    public Permission GetByName(string internalName)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var permission = c.Connection.QuerySingleOrDefault<Permission>("select * from `Permissions` where `InternalName` = @internalName", new
        {
            internalName
        });

        return permission;
    }

    public async Task<bool> HasPermissionAsync(User u, string internalName)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var pid = GetByName(internalName).Id;
        var usr = await _userRepository.GetByIDAsync(u.Id);

        var hasUserPermission = c.Connection.ExecuteScalar<bool>("select Count(1) from `User_Permissions` where `UserId` = @uid and `PermissionId` = @pid limit 1", new
        {
            uid = usr.Id,
            pid
        });

        if (hasUserPermission)
            return true;

        var hasPermission = c.Connection.ExecuteScalar<bool>("select Count(1) from `Role_Permissions` where `RoleId` = @rid and `PermissionId` = @pid limit 1", new
        {
            rid = usr.RoleId,
            pid
        });

        return hasPermission;
    }

    public async Task<IEnumerable<Permission>> GetAllForUserAsync(User u)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var usr = await _userRepository.GetByIDAsync(u.Id);

        return c.Connection.Query<Permission>("SELECT `Id`, `Name`, `InternalName` FROM ((Select up.PermissionId From `User_Permissions` " +
            "as up where up.UserId = @uid) UNION (Select rp.PermissionId From `Role_Permissions` as rp where rp.RoleId = @rid)) " +
            "as a left JOIN `Permissions` on a.PermissionId = Permissions.Id;", new
            {
                uid = usr.Id,
                rid = usr.RoleId
            });
    }
}