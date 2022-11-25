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
        var defaultAdminRoleId = NewRole("System Admin", "Default admin role");
        var administration_users_list = NewPermission("List Users", "administration_users_list");
        var administration_users_get = NewPermission("Get User", "administration_users_get");

        AddRolePermission(defaultAdminRoleId, administration_users_list);
        AddRolePermission(defaultAdminRoleId, administration_users_get);

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

    private Guid NewRole(string name, string description)
    {
        var guid = Guid.NewGuid();
        Insert.IntoTable("Roles").Row(new Role
        {
            Id = guid,
            Name = name,
            Description = description
        });
        return guid;
    }

    private void AddRolePermission(Guid roleId, Guid permissionId)
    {
        Insert.IntoTable("Role_Permissions").Row(new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        });
    }
}