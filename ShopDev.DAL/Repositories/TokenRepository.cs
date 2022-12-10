using DapperExtensions;
using ShopDev.DAL.Models;
using ShopDev.DAL.Utility;

namespace ShopDev.DAL.Repositories;

public class TokenRepository : RepositoryBase<Token>
{
    public TokenRepository(Models.Database db) : base(db) { }

    public async Task<bool> ValidateTokenAsync(Guid token, Guid userID, TokenType type)
    {
        using var c = new MySQLConnectionWrapper(DB.ConnString);
        var pq = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
        pq.Predicates.Add(Predicates.Field<Token>(t => t.Id, Operator.Eq, token));
        pq.Predicates.Add(Predicates.Field<Token>(t => t.UserID, Operator.Eq, userID));
        pq.Predicates.Add(Predicates.Field<Token>(t => t.Type, Operator.Eq, type));
        pq.Predicates.Add(Predicates.Field<Token>(t => t.ExpireTime, Operator.Gt, DateTime.UtcNow));
        return await c.Connection.CountAsync<Token>(pq) == 1;
    }
}