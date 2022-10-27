using Dapper;

namespace ShopDev.DAL.Models;

public class Database
{
    public string ConnString { get; }

    public Database(string connString)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        ConnString = connString;
    }
}