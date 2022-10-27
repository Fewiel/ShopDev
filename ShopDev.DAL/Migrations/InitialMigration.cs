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
            .WithColumn("Value").AsString().Nullable()
            .WithColumn("DisplayType").AsString();

        Create.Table("Logs")
            .WithColumn("ID").AsGuid().PrimaryKey()
            .WithColumn("Source").AsString()
            .WithColumn("Message").AsString()
            .WithColumn("Timestamp").AsDateTime();

        Create.Table("Users")
            .WithColumn("ID").AsGuid().PrimaryKey()
            .WithColumn("Email").AsString()
            .WithColumn("Username").AsString()
            .WithColumn("Name").AsString().Nullable()
            .WithColumn("SlackID").AsString().Nullable()
            .WithColumn("Password").AsString()
            .WithColumn("SSHPublicKey").AsString().Nullable()
            .WithColumn("Active").AsBoolean()
            .WithColumn("LastUsed").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("ExpirationDate").AsDateTime().Nullable()
            .WithColumn("RoleID").AsGuid().Nullable()
            .WithColumn("AbsenceDate").AsDateTime().Nullable()
            .WithColumn("AbsenceReason").AsString().Nullable()
            .WithColumn("AdminNote").AsString().Nullable();
    }
}