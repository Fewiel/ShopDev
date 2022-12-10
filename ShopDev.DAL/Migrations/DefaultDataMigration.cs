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
}