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
        var permission = c.Connection.QuerySingleOrDefault<Permission>("select * from `permissions` where `InternalName` = @internalName", new
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

        var hasUserPermission = c.Connection.ExecuteScalar<bool>("select Count(1) from `users_permissions` where `UserID` = @uid and `PermissionID` = @pid limit 1", new
        {
            uid = usr.Id,
            pid
        });

        if (hasUserPermission)
            return true;

        var hasPermission = c.Connection.ExecuteScalar<bool>("select Count(1) from `role_permissions` where `RoleID` = @rid and `PermissionID` = @pid limit 1", new
        {
            rid = usr.RoleID,
            pid
        });

        return hasPermission;
    }

    public async Task<IEnumerable<Permission>> GetAllForUserAsync(User u)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var usr = await _userRepository.GetByIDAsync(u.Id);

        return c.Connection.Query<Permission>("SELECT `ID`, `Name`, `InternalName` FROM ((Select up.PermissionID From `users_permissions` " +
            "as up where up.UserID = @uid) UNION (Select rp.PermissionID From `role_permissions` as rp where rp.RoleID = @rid)) " +
            "as a left JOIN `permissions` on a.PermissionID = permissions.ID;", new
            {
                uid = usr.Id,
                rid = usr.RoleID
            });
    }
}