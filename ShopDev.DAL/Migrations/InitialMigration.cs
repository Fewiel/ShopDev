using FluentMigrator;
using ShopDev.DAL.Models;

namespace ShopDev.DAL.Migrations;

[Migration(0)]
public class InitialMigration : Migration
{
    public override void Down()
    {
        Delete.Table("Settings");
    }

    public override void Up()
    {
        Create.Table("Settings")
            .WithColumn("ID").AsGuid().PrimaryKey()
            .WithColumn("DisplayName").AsString()
            .WithColumn("Value").AsString()
            .WithColumn("DisplayType").AsString();

        Create.Table("Logs")
            .WithColumn("ID").AsGuid().PrimaryKey()
            .WithColumn("Source").AsString()
            .WithColumn("Message").AsString()
            .WithColumn("Timestamp").AsDateTime();
    }
}