using FluentMigrator;
using ShopDev.DAL.Models;

namespace ShopDev.DAL.Migrations;

public class DefaultDataMigration : Migration
{
    public override void Down()
    {
        throw new NotImplementedException();
    }

    public override void Up()
    {
        Insert.IntoTable("Settings").Row(new Setting
        {
            ID = Guid.NewGuid(),
            Key = "domain",
            DisplayName = "Server Domain",
            Value = "shopdev.local",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            ID = Guid.NewGuid(),
            Key = "smtp_host",
            DisplayName = "SMTP Hostname",
            Value = "localhost",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            ID = Guid.NewGuid(),
            Key = "smtp_port",
            DisplayName = "SMTP Port",
            Value = "25",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            ID = Guid.NewGuid(),
            Key = "smtp_user",
            DisplayName = "SMTP Username",
            Value = "",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            ID = Guid.NewGuid(),
            Key = "smtp_mail",
            DisplayName = "SMTP Mail",
            Value = "local@shopdev.local",
            DisplayType = "text"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            ID = Guid.NewGuid(),
            Key = "smtp_password",
            DisplayName = "SMTP Password",
            Value = "",
            DisplayType = "password"
        });

        Insert.IntoTable("Settings").Row(new Setting
        {
            ID = Guid.NewGuid(),
            Key = "smtp_ssl",
            DisplayName = "SMTP SSL",
            Value = "True",
            DisplayType = "text"
        });
    }
}
