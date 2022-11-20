using Dapper;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;
using System.Collections.Generic;

namespace ShopDev.DAL.Repositories;

public class LimitRepository : RepositoryBase<Limit>
{
    private readonly UserLimitRepository UserLimitRepository;
    private readonly RoleLimitRepository RoleLimitRepository;

    public LimitRepository(Database db, UserLimitRepository userLimitRepository, RoleLimitRepository roleLimitRepository) : base(db)
    {
        UserLimitRepository = userLimitRepository;
        RoleLimitRepository = roleLimitRepository;
    }

    public async Task<Limit> GetByNameAsync(string internalName)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        return await c.Connection.QueryFirstOrDefaultAsync<Limit>("select * from `Limits` where `InternalName` = @internalName;", new
        {
            internalName
        });

    }

    public async Task<int> GetLimitAsync(User usr, string internalName)
    {
        var limit = await GetByNameAsync(internalName);
        var userLimit = await UserLimitRepository.GetForUserAndLimitAsync(usr.Id, limit.Id);

        if (userLimit != null)
            return userLimit.Value;

        var roleLimit = await RoleLimitRepository.GetForRoleAndLimitAsync(usr.RoleId, limit.Id);

        if (roleLimit != null)
            return roleLimit.Value;

        return 0;
    }
}