using FluentMigrator;
using ShopDev.DAL.Models;

namespace ShopDev.DAL.Migrations;

[Migration(0)]
public class InitialMigration : Migration
{
    public override void Down()
    {
        Delete.Table("Settings");
        Delete.Table("Logs");
        Delete.Table("Users");
        Delete.Table("Tokens");
        Delete.Table("Roles");
        Delete.Table("Role_Limits");
        Delete.Table("Role_Permissions");
        Delete.Table("User_Limits");
        Delete.Table("User_Permissions");
    }

    public override void Up()
    {
        Create.Table("Settings")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Key").AsString()
            .WithColumn("DisplayName").AsString()
            .WithColumn("Value").AsString().Nullable()
            .WithColumn("DisplayType").AsString();

        Create.Table("Logs")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Source").AsString()
            .WithColumn("Message").AsString()
            .WithColumn("Timestamp").AsDateTime();

        Create.Table("Users")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Email").AsString()
            .WithColumn("Username").AsString()
            .WithColumn("Name").AsString().Nullable()
            .WithColumn("SlackId").AsString().Nullable()
            .WithColumn("Password").AsString()
            .WithColumn("SSHPublicKey").AsString().Nullable()
            .WithColumn("Active").AsBoolean()
            .WithColumn("LastUsed").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("ExpirationDate").AsDateTime().Nullable()
            .WithColumn("RoleId").AsGuid().Nullable()
            .WithColumn("AbsenceDate").AsDateTime().Nullable()
            .WithColumn("AbsenceReason").AsString().Nullable()
            .WithColumn("AdminNote").AsString().Nullable();

        Create.Table("Tokens")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("UserId").AsGuid().Indexed()
            .WithColumn("ExpireTime").AsDateTime()            
            .WithColumn("Type").AsInt16();

        Create.Table("Permissions")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("InternalName").AsString();

        Create.Table("Limits")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("InternalName").AsString();

        Create.Table("Roles")
            .WithColumn("Id").AsGuid()
            .WithColumn("Name").AsString()
            .WithColumn("Description").AsString();            

        Create.Table("Role_Limits")
            .WithColumn("RoleId").AsGuid().Indexed()
            .WithColumn("LimitId").AsGuid().Indexed()
            .WithColumn("Value").AsInt32();

        Create.Table("Role_Permissions")
            .WithColumn("RoleId").AsGuid().Indexed()
            .WithColumn("PermissionId").AsGuid().Indexed();

        Create.Table("User_Limits")
            .WithColumn("UserId").AsGuid()
            .WithColumn("LimitId").AsGuid()
            .WithColumn("Value").AsInt32();

        Create.Table("User_Permissions")
            .WithColumn("UserId").AsGuid().Indexed()
            .WithColumn("PermissionId").AsGuid().Indexed();            
    }
}