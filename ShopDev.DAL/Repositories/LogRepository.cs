using ShopDev.DAL.Models;

namespace ShopDev.DAL.Repositories;

public class LogRepository : RepositoryBase<Log>
{
    public LogRepository(Database db) : base(db) { }
}