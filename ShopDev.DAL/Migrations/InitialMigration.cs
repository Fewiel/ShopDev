using FluentMigrator;

namespace ShopDev.DAL.Migrations;

[Migration(0)]
public class InitialMigration : Migration
{
    public override void Down()
    {
        Delete.Table("Settings");
    }


    //    case "ID": return ID;
    //case "Key": return Key;
    //case "DisplayName": return DisplayName;
    //case "Value": return Value;
    //case "DisplayType": return DisplayType;
    //default: return null;

    public override void Up()
    {
        Create.Table("Settings")
            .WithColumn("ID").AsBinary().PrimaryKey().Identity()
            .WithColumn("DisplayName").AsString()
            .WithColumn("Value").AsString()
            .WithColumn("DisplayType").AsString();
    }
}