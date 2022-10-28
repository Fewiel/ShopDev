using DapperExtensions;
using ShopDev.DAL.Models;
using ShopDev.DAL.Schema;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class UserRepository : RepositoryBase<User>
{
    public UserRepository(Models.Database db) : base(db) { }

    public async Task<User?> GetByUsernameAsync(string usrName)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var predicate = Predicates.Field<User>(f => f.Username, Operator.Eq, usrName);
        return (await c.Connection.GetListAsync<User>(predicate)).FirstOrDefault();
    }
}