using ShopDev.DAL.Models;
using ShopDev.DAL.Schema;

namespace ShopDev.DAL.Repositories;

public class SettingRepository : RepositoryBase<Setting>
{
    public SettingRepository(Database db) : base(db) { }
}