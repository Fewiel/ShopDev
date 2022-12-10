using Dapper;
using DapperExtensions;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class PermissionRepository : RepositoryBase<Permission>
{
    private const int MaxPermissionDepth = 10;
    private readonly UserRepository _userRepository;

    public PermissionRepository(Models.Database db, UserRepository userRepository) : base(db)
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

        var hasUserPermission = c.Connection.ExecuteScalar<bool>("select Count(1) from `UserPermissions` where `UserId` = @uid and `PermissionId` = @pid limit 1", new
        {
            uid = usr.Id,
            pid
        });

        if (hasUserPermission)
            return true;

        return await RoleContainsPermissionAsync(usr.RoleId, c, pid);
    }

    private async Task<bool> RoleContainsPermissionAsync(Guid roleId, MySQLConnectionWrapper c, Guid permissionId, int depth = 0)
    {
        if (depth >= MaxPermissionDepth)
            return false;

        var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
        pg.Predicates.Add(Predicates.Field<RolePermission>(rp => rp.RoleId, Operator.Eq, roleId));
        pg.Predicates.Add(Predicates.Field<RolePermission>(rp => rp.PermissionId, Operator.Eq, permissionId));

        if (await c.Connection.CountAsync<RolePermission>(pg) == 1)
            return true;

        var role = await c.Connection.GetAsync<Role>(roleId);
        if (role.ParentId != null)
            return await RoleContainsPermissionAsync(role.ParentId.Value, c, permissionId, depth++);

        return false;
    }

    public async Task<IEnumerable<Permission>> GetAllForUserAsync(User u)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var usr = await _userRepository.GetByIDAsync(u.Id);

        return c.Connection.Query<Permission>("SELECT `Id`, `Name`, `InternalName` FROM ((Select up.PermissionId From `UserPermissions` " +
            "as up where up.UserId = @uid) UNION (Select rp.PermissionId From `RolePermissions` as rp where rp.RoleId = @rid)) " +
            "as a left JOIN `Permissions` on a.PermissionId = Permissions.Id;", new
            {
                uid = usr.Id,
                rid = usr.RoleId
            });
    }
}