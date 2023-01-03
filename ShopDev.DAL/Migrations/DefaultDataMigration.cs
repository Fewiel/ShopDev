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
        var defaultUserRoleId = NewRole("Default User", "Default user role");
        AddRolePermission(defaultUserRoleId, NewPermission("Get Roles", "roles_get"));

        var defaultAdminRoleId = NewRole("System Admin", "Default admin role", defaultUserRoleId);
        AddRolePermission(defaultAdminRoleId, NewPermission("List Users", "administration_users_list"));
        AddRolePermission(defaultAdminRoleId, NewPermission("Get User", "administration_users_get"));
        AddRolePermission(defaultAdminRoleId, NewPermission("Get User", "administration_users_add"));
        AddRolePermission(defaultAdminRoleId, NewPermission("Lock User", "administration_users_lock"));
        AddRolePermission(defaultAdminRoleId, NewPermission("Delete User", "administration_users_delete"));
        AddRolePermission(defaultAdminRoleId, NewPermission("Delete User", "administration_users_update"));

        AddSetting("Node Server Port", "text", "node_server_port", "34152");
        AddSetting("Interface Domain", "text", "domain_interface", "notset.tld");
        AddSetting("Registration Mail", "text", "mail_registration",
            @"A ShopDev account has been created for you!

You can now login to {0} with the following credentials:  

Username: {1}
Password: {2}

Please remember to change your password after logging in.");

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

        var countTestusers = 30;
        for (int i = 0; i < countTestusers; i++)
        {
            Insert.IntoTable("Users").Row(new User
            {
                Id = Guid.NewGuid(),
                Username = $"Testuser{i}",
                Email = "admin@shopdev.local",
                Password = PasswordHasher.Hash($"Testuser{i}"),
                RoleId = defaultUserRoleId,
                Active = true
            });
        }
    }

    private Guid NewPermission(string name, string internalName)
    {
        var guid = Guid.NewGuid();
        Insert.IntoTable("Permissions").Row(new Permission
        {
            Id = guid,
            Name = name,
            InternalName = internalName
        });
        return guid;
    }

    private Guid NewRole(string name, string description, Guid? parentId = null)
    {
        var guid = Guid.NewGuid();
        Insert.IntoTable("Roles").Row(new Role
        {
            Id = guid,
            ParentId = parentId,
            Name = name,
            Description = description
        });
        return guid;
    }

    private void AddRolePermission(Guid roleId, Guid permissionId)
    {
        Insert.IntoTable("RolePermissions").Row(new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        });
    }

    private void AddSetting(string displayName, string displayType, string key, string value)
    {
        Insert.IntoTable("Settings").Row(new Setting
        {
            Id = Guid.NewGuid(),
            DisplayName = displayName,
            DisplayType = displayType,
            Key = key,
            Value = value
        });
    }
}