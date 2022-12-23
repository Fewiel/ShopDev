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

    public async Task<User?> GetByEmailAsync(string usrEmail)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var predicate = Predicates.Field<User>(f => f.Email, Operator.Eq, usrEmail);
        return (await c.Connection.GetListAsync<User>(predicate)).FirstOrDefault();
    }

    public async Task<bool> UserExistsAsync(User usr)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var pq = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
        pq.Predicates.Add(Predicates.Field<User>(t => t.Username, Operator.Eq, usr.Username));
        pq.Predicates.Add(Predicates.Field<User>(t => t.Email, Operator.Eq, usr.Email));
        return await c.Connection.CountAsync<User>(pq) == 1;
    }
}