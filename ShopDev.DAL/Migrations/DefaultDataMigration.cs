using FluentMigrator;
using ShopDev.DAL.Models;

namespace ShopDev.DAL.Migrations;

[Migration(1)]
public class DefaultDataMigration : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
        //Add Default Role, Permissions and Limits
        var defaultAdminPermissionId = Guid.NewGuid();
        Insert.IntoTable("Permissions").Row(new Permission
        {
            Id = defaultAdminPermissionId,
            Name = "System Administrator",
            InternalName = "sys_admin"
        });

        var defaultAdminRoleId = Guid.NewGuid();
        Insert.IntoTable("Roles").Row(new Role
        {
            Id = defaultAdminRoleId,
            Name = "System Admin",
            Description = "System Administrator - All rights"
        });

        Insert.IntoTable("Role_Permissions").Row(new RolePermission
        {
            RoleId = defaultAdminRoleId,
            PermissionId = defaultAdminPermissionId
        });

        var mngUserPermId = Guid.NewGuid();
        Insert.IntoTable("Permissions").Row(new Permission
        {
            Id = mngUserPermId,
            Name = "Manage Users",
            InternalName = "manage_users"
        });

        Insert.IntoTable("Role_Permissions").Row(new RolePermission
        {
            RoleId = defaultAdminRoleId,
            PermissionId = mngUserPermId
        });

        Insert.IntoTable("Limits").Row(new Limit
        {
            Id = Guid.NewGuid(),
            Name = "Max Environments",
            InternalName = "environment_max"
        });

        Insert.IntoTable("Limits").Row(new Limit
        {
            Id = Guid.NewGuid(),
            Name = "Max Permanent Environments",
            InternalName = "environments_max_perm"
        });

        Insert.IntoTable("Limits").Row(new Limit
        {
            Id = Guid.NewGuid(),
            Name = "Environment stored after X Days",
            InternalName = "enviroment_storetime"
        });

        Insert.IntoTable("Limits").Row(new Limit
        {
            Id = Guid.NewGuid(),
            Name = "Environment deleted after X Days",
            InternalName = "enviroment_deletetime"
        });

        Insert.IntoTable("Limits").Row(new Limit
        {
            Id = Guid.NewGuid(),
            Name = "Max Docker Containers",
            InternalName = "docker_max"
        });

        //Add Default User
        var defaultUserId = Guid.NewGuid();
        Insert.IntoTable("Users").Row(new User
        {
            Id = defaultUserId,
            Username = "admin",
            Email = "admin@shopdev.local",
            Password = PasswordHasher.Hash("admin"),
            RoleId = defaultAdminRoleId,
            Active = true
        });

        //Add Default Settings
        Insert.IntoTable("Settings").Row(new Setting
        {
            Id = Guid.NewGuid(),
            Key = "domain",
            DisplayName = "Server Domain",
            Value = "shopdev.local",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            Id = Guid.NewGuid(),
            Key = "smtp_host",
            DisplayName = "SMTP Hostname",
            Value = "localhost",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            Id = Guid.NewGuid(),
            Key = "smtp_port",
            DisplayName = "SMTP Port",
            Value = "25",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            Id = Guid.NewGuid(),
            Key = "smtp_user",
            DisplayName = "SMTP Username",
            Value = "",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            Id = Guid.NewGuid(),
            Key = "smtp_mail",
            DisplayName = "SMTP Mail",
            Value = "local@shopdev.local",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            Id = Guid.NewGuid(),
            Key = "smtp_password",
            DisplayName = "SMTP Password",
            Value = "",
            DisplayType = "password"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            Id = Guid.NewGuid(),
            Key = "smtp_ssl",
            DisplayName = "SMTP SSL",
            Value = "True",
            DisplayType = "text"
        });
    }
}
