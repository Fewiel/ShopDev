using ShopDev.DAL.Models;
using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Repositories;

public class UserRepository : RepositoryBase<User>
{
    public UserRepository(Database db) : base(db)
    {
    }

    protected override TableSchema TableSchema => throw new NotImplementedException();
}