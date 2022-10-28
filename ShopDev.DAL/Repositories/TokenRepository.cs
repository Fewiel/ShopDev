using ShopDev.DAL.Models;

namespace ShopDev.DAL.Repositories;

public class TokenRepository : RepositoryBase<Token>
{
    public TokenRepository(Database db) : base(db) { }
}